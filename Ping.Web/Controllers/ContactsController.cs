using Inertia.NET.Extensions;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ping.Core.Extensions.Pagination;
using Ping.Data;
using Ping.Data.Models;

namespace Ping.Web.Controllers;

[Route("contacts")]
public class ContactsController : Controller
{
    private readonly PingDbContext _db;

    public ContactsController(PingDbContext db)
    {
        _db = db;
    }

    public IActionResult Index([FromQuery] PaginationQuery query)
    {
        var contactsPaginationResult = _db.Contacts
            .Include(i => i.Organization)
            .AsNoTracking()
            .Select(s => new Contact
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Phone = s.Phone,
                City = s.City,
                Organization = new Organization
                {
                    Id = s.Organization.Id,
                    Name = s.Organization.Name
                }
            })
            .Paginate<Contact>(
                query,
                w =>
                    w.FirstName.ToLower().Contains(query.search.ToLower()) ||
                    w.LastName.ToLower().Contains(query.search.ToLower()) ||
                    w.Organization.Name.ToLower().Contains(query.search.ToLower()) ||
                    w.City.ToLower().Contains(query.search.ToLower()) ||
                    w.Phone.ToLower().Contains(query.search.ToLower())
            );
        return this.InertiaRender("Contacts/Index", contactsPaginationResult);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var organizations = await GetOrganizations();
        return this.InertiaRender("Contacts/Create", new { organizations });
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] Contact contact)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.GetInertiaErrors();
            var organizations = await GetOrganizations();
            return this.InertiaRender("Contacts/Create", new { errors, organizations });
        }

        _db.Contacts.Add(contact);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{contactId:int}/edit")]
    public async Task<IActionResult> Edit([FromRoute] int contactId)
    {
        var contact = await _db.Contacts
            .AsNoTracking()
            .Select(s => new Contact
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                City = s.City,
                State = s.State,
                ZipCode = s.ZipCode,
                OrganizationId = s.OrganizationId
            })
            .FirstOrDefaultAsync(f => f.Id == contactId);
        var organizations = await GetOrganizations();

        return contact is not null
            ? this.InertiaRender("Contacts/Edit", new { contact, organizations })
            : NotFound();
    }

    [HttpPut("{contactId:int}/edit")]
    public async Task<IActionResult> Edit([FromRoute] int contactId, [FromBody] Contact organization)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.GetInertiaErrors();
            var organizations = await GetOrganizations();
            return this.InertiaRender("Contacts/Edit", new { errors, organizations });
        }

        var currentContact = await _db.Contacts
            .FirstOrDefaultAsync(f => f.Id == contactId);

        if (currentContact is null)
            return NotFound();

        organization.Adapt(currentContact);
        _db.Contacts.Update(currentContact);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<List<Organization>> GetOrganizations()
    {
        return await _db.Organizations
            .AsNoTracking()
            .Select(s => new Organization
            {
                Id = s.Id,
                Name = s.Name
            })
            .ToListAsync();
    }
}
