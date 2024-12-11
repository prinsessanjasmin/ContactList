using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs;

public class ContactDto(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postCode, string city)
{
    public string FirstName { get; set; } = firstName.Trim();
    public string LastName { get; set; } = lastName.Trim();
    public string Email { get; set; } = email.Trim();
    public string PhoneNumber { get; set; } = phoneNumber.Trim();
    public string StreetAddress { get; set; } = streetAddress.Trim();
    public string PostCode { get; set; } = postCode.Trim();
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
