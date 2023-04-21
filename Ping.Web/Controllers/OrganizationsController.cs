using Inertia.NET.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ping.Data;

namespace Ping.Web.Controllers;

public class OrganizationsController : Controller
{
    private readonly PingDbContext _db;

    public OrganizationsController(PingDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var organizations = await _db.Organizations.Take(10).ToListAsync();
        return this.InertiaRender("Organizations/Index", new { organizations });
    }
}
