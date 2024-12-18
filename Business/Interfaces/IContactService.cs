using Business.DTOs;
using Business.Models;

namespace Business.Interfaces
{
    public interface IContactService
    {
        bool CreateNewContact(ContactDto dto);
        List<Contact> ViewAllContacts();
    }
}