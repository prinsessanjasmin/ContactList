using Business.Models;

namespace Business.Interfaces
{
    public interface IContactServiceCRUD : IContactService
    {
        bool UpdateContact(Contact contact);
        bool DeleteContact(Contact contact);
        Contact FindContactById(string id);
    }
}