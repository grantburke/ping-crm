using Inertia.NET.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ping.Web.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return this.InertiaRender("Dashboard/Index");
    }
}
