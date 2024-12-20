
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs;

public class ContactDto(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postCode, string city)
{
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

    public static ContactDto CreateEmpty()
    {
        return new ContactDto(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
    }
}
