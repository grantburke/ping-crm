using Microsoft.EntityFrameworkCore;
using Ping.Data.Models;

namespace Ping.Data;

public class PingDbContext : DbContext
{
    public PingDbContext(DbContextOptions<PingDbContext> options)
        : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ping");
        modelBuilder.Entity<User>()
            .HasIndex(i => i.Email)
            .IsUnique();
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        OnBeforeSaving();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();
        var now = DateTime.Now;

        foreach (var entry in entries)
        {
            // for entities that inherit from Timestamps, set UpdatedOn / CreatedOn appropriately
            if (entry.Entity is Timestamps trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        // set the updated date to "now"
                        trackable.UpdatedAt = now;
                        // mark property as "don't touch"
                        // we don't want to update on a Modify operation
                        entry.Property("CreatedAt").IsModified = false;
                        break;
                    case EntityState.Added:
                        // set created/updated date to "now"
                        trackable.CreatedAt = now;
                        trackable.UpdatedAt = now;
                        break;
                }
            }
        }
    }
}
