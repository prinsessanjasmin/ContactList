using Business.DTOs;
using Business.Models;

namespace Business.Interfaces
{
    public interface IContactService
    {
        bool CreateNewContact(List<Contact> contactList, ContactDto dto);
        bool ViewAllContacts(List<Contact> contactList);
    }
}