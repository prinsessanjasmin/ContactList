using Business.DTOs;
using Business.Models;

namespace Business.Interfaces
{
    public interface IContactFactoryService
    {
        Contact CreateContact(ContactDto dto);
        ContactDto CreateContactDto(Contact contact);
    }
}