using Bogus;
using Ping.Core.Utils;
using Ping.Data;
using Ping.Data.Models;
using static Bogus.DataSets.Name;

namespace Ping.Core.Extensions;

public static class DbExtensions
{
    public static void SeedDb(this PingDbContext db)
    {
        if (db.Organizations.Count() == 0
            && db.Contacts.Count() == 0
            && db.Users.Count() == 0)
        {
            db.Users.Add(new User
            {
                Id = 0,
                Email = "johndoe@test.com",
                Password = PasswordHasher.HashPassword("Pass123$"),
                Role = Role.Owner
            });

            db.Users.Add(new User
            {
                Id = 0,
                Email = "janedoe@test.com",
                Password = PasswordHasher.HashPassword("Pass123$"),
                Role = Role.User
            });

            var contact = new Faker<Contact>()
                .RuleFor(m => m.FirstName, f => f.Name.FirstName(f.PickRandom<Gender>()))
                .RuleFor(m => m.LastName, f => f.Name.LastName(f.PickRandom<Gender>()))
                .RuleFor(m => m.Email, f => f.Internet.Email())
                .RuleFor(m => m.Phone, f => f.Phone.PhoneNumber("###-###-####"))
                .RuleFor(m => m.Address, f => f.Address.StreetAddress())
                .RuleFor(m => m.City, f => f.Address.City())
                .RuleFor(m => m.State, f => f.Address.State())
                .RuleFor(m => m.ZipCode, f => f.Address.ZipCode("#####"));

            var org = new Faker<Organization>()
                .RuleFor(m => m.Name, f => f.Company.CompanyName())
                .RuleFor(m => m.Email, f => f.Internet.Email())
                .RuleFor(m => m.Phone, f => f.Phone.PhoneNumber("###-###-####"))
                .RuleFor(m => m.Address, f => f.Address.StreetAddress())
                .RuleFor(m => m.City, f => f.Address.City())
                .RuleFor(m => m.State, f => f.Address.State())
                .RuleFor(m => m.ZipCode, f => f.Address.ZipCode("#####"))
                .RuleFor(m => m.Contacts, f => contact.Generate(3).ToList());

            var orgs = org.Generate(50);
            db.Organizations.AddRange(orgs);
            db.SaveChanges();
        }
    }
}