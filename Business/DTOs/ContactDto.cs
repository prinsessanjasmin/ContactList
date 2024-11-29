using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs;

public class ContactDto
{
    
    public string ContactName { get; set; } = null!; 
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;


    public static ContactDto ToDto(Contact contact)
    {
        return new ContactDto
        {
            ContactName = ($"{contact.FirstName} {contact.LastName}"),
            Email = contact.Email,
            Address = ($"{contact.StreetAddress}, {contact.PostCode} {contact.City}")
        };
    }
}
