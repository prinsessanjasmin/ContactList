using Business.Models;
using Business.Factories;
using Business.Interfaces;
using Business.DTOs;

namespace Business.Services;

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
