using Business.Models;
using Business.Services;

namespace Business.Factories;

internal class ContactFactory
{
    public static Contact Create(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postCode, string city)
    {   
        return new Contact
        {
            Id = Helpers.CreateUniqueId(),
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Email = email.Trim(),
            PhoneNumber = phoneNumber.Trim(),
            StreetAddress = streetAddress.Trim(),
            PostCode = postCode.Trim(),
            City = city.Trim()
        };
    }
}
