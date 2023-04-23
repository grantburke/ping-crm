using Inertia.NET.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ping.Core.Extensions.Pagination;
using Ping.Data;
using Ping.Data.Models;

namespace Ping.Web.Controllers;

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
                Organization = new Organization
                {
                    Id = s.Organization.Id,
                    Name = s.Organization.Name
                },
                City = s.City,
                Phone = s.Phone
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
}
