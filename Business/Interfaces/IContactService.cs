using Business.DTOs;
using Business.Models;

namespace Business.Interfaces
{
    public interface IContactService
    {
        void CreateNewContact(List<Contact> contactList, ContactDto dto);
        void ViewAllContacts(List<Contact> contactList);
    }
}