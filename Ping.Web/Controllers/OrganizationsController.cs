using Inertia.NET.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ping.Core.Extensions.Pagination;
using Ping.Data;
using Ping.Data.Models;

namespace Ping.Web.Controllers;

public class OrganizationsController : Controller
{
    private readonly PingDbContext _db;

    public OrganizationsController(PingDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index([FromQuery] PaginationQuery query)
    {
        var orgPaginationResult = await _db.Organizations
            .AsNoTracking()
            .Paginate<Organization>(query);
        return this.InertiaRender("Organizations/Index", orgPaginationResult);
    }
}