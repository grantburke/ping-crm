using System.Diagnostics;
using Inertia.NET.Extensions;
using Microsoft.AspNetCore.Mvc;
using Ping.Data;
using Ping.Web.ViewModels;

namespace Ping.Web.Controllers;

public class HomeController : Controller
{
    private readonly PingDbContext _db;

    public HomeController(PingDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        // var orgs = _db.Organizations.ToList();
        return this.InertiaRender("Home/Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
