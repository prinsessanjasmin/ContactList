using Business.Models;

namespace Business.Interfaces
{
    public interface IContactService
    {
        void CreateNewContact(List<Contact> contactList);

        void ViewAllContacts(List<Contact> contactList);
    }
}