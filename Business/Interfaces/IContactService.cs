using Business.DTOs;
using Business.Models;

namespace Business.Interfaces;
/// <summary>
/// This interface is the base for the ContactService, and only promises the two methods used by the Console Application.
/// </summary>

public interface IContactService
{
    bool CreateNewContact(ContactDto dto);
    List<Contact> ViewAllContacts();

    
}