using Business.Models;
using Business.DTOs;

namespace Business.Factories;

/// <summary>
/// The factory has two methods, one to use a ContactDto to make a Contact (and provide a unique id if it doesn't already have one), 
/// and one to convert a Contact into a ContactDto. The second conversion is used when a user wants to edit the contact. If so, I don't want 
/// them to be able to input invalid values, and as all the validation happens in the ContactDto class this is also done there.
/// </summary>
public class ContactFactory
{ 
    public static Contact CreateContact(ContactDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        return new Contact
        {
            Id = dto.Id ?? Guid.NewGuid().ToString(),
            FirstName = dto.FirstName.Trim(),
            LastName = dto.LastName.Trim(),
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            StreetAddress = dto.StreetAddress.Trim(),
            PostCode = dto.PostCode.Trim(),
            City = dto.City.Trim(),

            DisplayName = $"{dto.FirstName.Trim()} {dto.LastName.Trim()}",
            Address = $"{dto.StreetAddress.Trim()} {dto.PostCode.Trim()} {dto.City.Trim()}",
        };
    }

    public static ContactDto CreateContactDto(Contact contact) 
    {
        if (contact == null) throw new ArgumentNullException(nameof(contact));

        return new ContactDto(
        
            contact.Id,
            contact.FirstName,
            contact.LastName,
            contact.Email,
            contact.PhoneNumber,
            contact.StreetAddress,
            contact.PostCode,
            contact.City
        );
    }
}
