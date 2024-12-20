using Business.Models;
using Business.Factories;
using Business.Interfaces;
using Business.DTOs;

namespace Business.Services;

public class ContactFactoryService : IContactFactoryService
{
    public Contact Create(ContactDto dto, string id)
    {
        return ContactFactory.Create(dto, id);
    }
}
