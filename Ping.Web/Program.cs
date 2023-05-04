using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Ping.Core.Extensions;
using Ping.Core.Infrastructure.Authentication;
using Ping.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ping.db");
builder.Services.AddDbContext<PingDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "ping.crm";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/login";
        options.LogoutPath = "/login";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthorizationHandler, UserIdRequirementClaimHandler>();
builder.Services
    .AddAuthorization(options =>
    {
        options.AddPolicy(PolicyConstants.Owner, policy => policy.RequireRole(PolicyConstants.Owner));
        options.AddPolicy(PolicyConstants.UserId, policy =>
        {
            policy.RequireUserIdClaim();
        });
    });
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
