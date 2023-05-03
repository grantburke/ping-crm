using Inertia.NET.Extensions;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ping.Core.Extensions.Pagination;
using Ping.Data;
using Ping.Data.Models;

namespace Ping.Web.Controllers;

[Authorize]
[Route("organizations")]
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

    [HttpGet("create")]
    public IActionResult Create()
        => this.InertiaRender("Organizations/Create");

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] Organization organization)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.GetInertiaErrors();
            return this.InertiaRender("Organizations/Create", new { errors });
        }

        _db.Organizations.Add(organization);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{organizationId:int}/edit")]
    public async Task<IActionResult> Edit([FromRoute] int organizationId)
    {
        var organization = await _db.Organizations
            .AsNoTracking()
            .Include(i => i.Contacts)
            .Select(s => new Organization
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                City = s.City,
                State = s.State,
                ZipCode = s.ZipCode,
            })
            .FirstOrDefaultAsync(f => f.Id == organizationId);
        var contacts = await GetContactsByOrganizationId(organizationId);
        return organization is not null
            ? this.InertiaRender("Organizations/Edit", new { organization, contacts })
            : NotFound();
    }

    [HttpPut("{organizationId:int}/edit")]
    public async Task<IActionResult> Edit([FromRoute] int organizationId, [FromBody] Organization organization)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.GetInertiaErrors();
            var contacts = await GetContactsByOrganizationId(organizationId);
            return this.InertiaRender("Organizations/Edit", new { errors, contacts });
        }

        var currentOrganization = await _db.Organizations
            .FirstOrDefaultAsync(f => f.Id == organizationId);

        if (currentOrganization is null)
            return NotFound();

        organization.Adapt(currentOrganization);
        _db.Organizations.Update(currentOrganization);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<List<Contact>> GetContactsByOrganizationId(int organizationId)
    {
        return await _db.Contacts
            .Where(w => w.OrganizationId == organizationId)
            .ToListAsync();
    }
}