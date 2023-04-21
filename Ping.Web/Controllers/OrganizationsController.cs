using Inertia.NET.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ping.Web.Controllers;

public class OrganizationsController : Controller
{
    public IActionResult Index()
    {
        return this.InertiaRender("Organizations/Index");
    }
}
