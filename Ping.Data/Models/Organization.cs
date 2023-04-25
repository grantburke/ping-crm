using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Ping.Data.Models;

public class Organization : BaseEntity
{
    [Required]
    public string Name { get; set; }

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

    [ValidateNever]
    public List<Contact> Contacts { get; set; } = new();
}