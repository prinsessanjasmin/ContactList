using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs;

public class ContactDto(string id, string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postCode, string city)
{
    [Required]
    public string Id { get; set; } = id; 
    [Required]
    public string FirstName { get; set; } = firstName.Trim();
    [Required]
    public string LastName { get; set; } = lastName.Trim();
    [Required]
    public string Email { get; set; } = email.Trim();
    [Required]
    public string PhoneNumber { get; set; } = phoneNumber.Trim();
    [Required]
    public string StreetAddress { get; set; } = streetAddress.Trim();
    [Required]
    public string PostCode { get; set; } = postCode.Trim();
    [Required]
    public string City { get; set; } = city.Trim();

    /*
    public static ContactDto ToDto(Contact contact)
    {
        return new ContactDto
        {
            ContactName = ($"{contact.FirstName} {contact.LastName}"),
            Email = contact.Email,
            Address = ($"{contact.StreetAddress}, {contact.PostCode} {contact.City}")
        };
    }
    */
}
