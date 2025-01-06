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
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            StreetAddress = dto.StreetAddress,
            PostCode = dto.PostCode,
            City = dto.City,

            DisplayName = dto.FirstName + " " + dto.LastName,
            Address = dto.StreetAddress + " " + dto.PostCode + " " + dto.City,
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
