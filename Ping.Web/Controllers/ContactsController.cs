using Inertia.NET.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ping.Web.Controllers;

public class ContactsController : Controller
{
    public IActionResult Index()
    {
        return this.InertiaRender("Contacts/Index");
    }
}
