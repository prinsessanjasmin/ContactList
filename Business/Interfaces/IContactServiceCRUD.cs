using Business.DTOs;
using Business.Models;

namespace Business.Interfaces
{
    public interface IContactServiceCRUD
    {
        bool CreateNewContact(ContactDto dto);
        bool UpdateContact(Contact contact);
        bool DeleteContact(Contact contact);

        IEnumerable<Contact> FindContact(string searchWord); 
        List<Contact> ViewAllContacts();
    }
}