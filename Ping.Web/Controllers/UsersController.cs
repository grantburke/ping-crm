using Inertia.NET.Extensions;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ping.Core.Infrastructure.Authentication;
using Ping.Core.Utils;
using Ping.Data;
using Ping.Data.Models;

namespace Ping.Web.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly PingDbContext _db;

    public UsersController(PingDbContext db)
    {
        _db = db;
    }
    
    [Authorize(Policy = PolicyConstants.Owner)]
    public async Task<IActionResult> Index()
    {
        var users = await _db.Users
            .Select(s => new User
            {
                Id = s.Id,
                Email = s.Email,
                Role = s.Role
            })
            .ToListAsync();

        var userDtos = users.Select(s => new UserDto(s.Id, s.Email, Enum.GetName(typeof(Role), s.Role)));
        return this.InertiaRender("Users/Index", new { users = userDtos });
    }

    [Authorize(Policy = PolicyConstants.Owner)]
    [HttpGet("create")]
    public IActionResult Create()
    {
        var roles = GetRoles();
        return this.InertiaRender("Users/Create", new { roles });
    }

    [Authorize(Policy = PolicyConstants.Owner)]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.GetInertiaErrors();
            var roles = GetRoles();
            return this.InertiaRender("Users/Create", new { errors, roles });
        }

        user.Password = PasswordHasher.HashPassword(user.Password);
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = PolicyConstants.UserId)]
    [HttpGet("{userId:int}/edit")]
    public async Task<IActionResult> Edit([FromRoute] int userId)
    {
        var user = await _db.Users
            .AsNoTracking()
            .Select(s => new User
            {
                Id = s.Id,
                Email = s.Email,
                Role = s.Role
            })
            .FirstOrDefaultAsync(f => f.Id == userId);

        if (user is null) return NotFound();

        var roles = GetRoles();
        return this.InertiaRender("Users/Edit", new { user, roles });
    }

    [Authorize(Policy = PolicyConstants.UserId)]
    [HttpPut("{userId:int}/edit")]
    public async Task<IActionResult> Edit([FromRoute] int userId, [FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.GetInertiaErrors();
            var roles = GetRoles();
            return this.InertiaRender("Users/Edit", new { errors, roles });
        }

        var currentUser = await _db.Users
            .FirstOrDefaultAsync(f => f.Id == userId);

        if (currentUser is null)
            return NotFound();

        user.Password = PasswordHasher.HashPassword(user.Password);
        user.Adapt(currentUser);
        _db.Users.Update(currentUser);
        await _db.SaveChangesAsync();

        return HttpContext.User.HasClaim(c => c.Type == PolicyConstants.UserId && c.Value == userId.ToString()) 
            ? RedirectToAction("Index", "Dashboard")
            : RedirectToAction(nameof(Index));
    }

    private List<RoleDto> GetRoles()
        => Enum.GetValues(typeof(Role))
            .Cast<object>()
            .ToDictionary(enumValue => enumValue.ToString(), enumValue => (int)enumValue)
            .Select(s => new RoleDto(s.Value, s.Key))
            .ToList();
}

public record UserDto(int Id, string Email, string Role);
public record RoleDto(int Id, string Name);
