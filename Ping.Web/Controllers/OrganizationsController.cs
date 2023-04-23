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

    public IActionResult Index([FromQuery] PaginationQuery query)
    {
        var orgPaginationResult = _db.Organizations
            .AsNoTracking()
            .Paginate<Organization>(
                query,
                w =>
                    w.Name.ToLower().Contains(query.search.ToLower()) ||
                    w.City.ToLower().Contains(query.search.ToLower()) ||
                    w.Phone.ToLower().Contains(query.search.ToLower())
            );
        return this.InertiaRender("Organizations/Index", orgPaginationResult);
    }
}