using System.ComponentModel.DataAnnotations;

namespace Ping.Data.Models;

public class Contact : BaseEntity
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string ZipCode { get; set; }

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
}