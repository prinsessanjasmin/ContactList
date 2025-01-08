using Business.Models;
using Business.Factories;
using Business.Interfaces;
using Business.DTOs;

namespace Business.Services;
/// <summary>
/// This service is just employing the already existent methods of the ContactFactory. It's put in a service so that it can have an interface, which simplifies testing of the functions.
/// </summary>
public class ContactFactoryService : IContactFactoryService
{
    public Contact CreateContact(ContactDto dto)
    {
        return ContactFactory.CreateContact(dto);
    }

    public ContactDto CreateContactDto(Contact Contact)
    {
        return ContactFactory.CreateContactDto(Contact);
    }
}
