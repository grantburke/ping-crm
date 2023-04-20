using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Ping.Core.Extensions;
using Ping.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ping.db");
builder.Services.AddDbContext<PingDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddRouting(opts => opts.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    #region SEED DB

    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;
        var db = services.GetRequiredService<PingDbContext>();
        db.Database.Migrate();
        db.SeedDb();
    }

    #endregion

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "Client/public")
        )
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
