using System.Security.Claims;
using Inertia.NET.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ping.Core.Utils;
using Ping.Data;
using Ping.Data.Models;
using Ping.Web.ViewModels;

namespace Ping.Web.Controllers;

public class AuthController : Controller
{
    private readonly PingDbContext _db;

    public AuthController(PingDbContext db)
    {
        _db = db;
    }

    [HttpGet("login")]
    public IActionResult Login()
        => this.InertiaRender("Auth/Login");

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel vm)
    {
        var errors = new Dictionary<string, List<string>>();
        if (!ModelState.IsValid)
        {
            errors = ModelState.GetInertiaErrors();
            return this.InertiaRender("Auth/Login", new { errors });
        }

        var user = await _db.Users.FirstOrDefaultAsync(f => f.Email == vm.Email);
        var verifiedPassword = PasswordHasher.VerifyPassword(vm.Password, user.Password);
        if (user is null || !verifiedPassword)
        {
            errors["general"] = new List<string> { "Invalid email or password" };
            return this.InertiaRender("Auth/Login", new { errors });
        }

        await HttpContext.SignInAsync(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, Enum.GetName(typeof(Role), user.Role))
                        },
                        CookieAuthenticationDefaults.AuthenticationScheme
                    )
                )
            );
        return RedirectToAction("Index", "Dashboard");
    }
}