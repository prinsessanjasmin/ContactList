using Business.Models;
using Business.DTOs;

namespace Business.Factories;

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
