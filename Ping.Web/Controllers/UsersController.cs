using Inertia.NET.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ping.Web.Controllers;

public class UsersController : Controller
{
    public IActionResult Index()
    {
        return this.InertiaRender("Users/Index");
    }
}
