using Business.Models;

namespace Business.Interfaces;

/// <summary>
/// This interface inherits from IContactService. All the methods here are specific to the WPF Application. 
/// </summary>

public interface IContactServiceCRUD : IContactService
{
    bool UpdateContact(Contact contact);
    bool DeleteContact(Contact contact);

    event EventHandler? ContactListUpdated;
}