using Business.DTOs;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;
using System.Linq; 
using System.Runtime.CompilerServices;

namespace Business.Services;
/// <summary>
/// The ContactService is the main class for executing the program's functions. It implements both the IContactService interface and the IContactServiceCRUD interface, 
/// so it caters to both the Console application and the WPF App. 
/// </summary>
/// 
public class ContactService : IContactService, IContactServiceCRUD
{
    private List<Contact> _contactList = [];
    private readonly IFileService _fileService;
    private readonly IContactFactoryService _contactFactoryService;

    public ContactService(IFileService fileService, IContactFactoryService contactFactoryService)
    {
        _fileService = fileService;
        _contactFactoryService = contactFactoryService;
        _contactList = _fileService.LoadListFromFile();
    }

    /// <summary>
    /// The logic in the method is encased in a try/catch statement, in case anything should go wrong. 
    /// The method first makes sure the list of contacts is updated, then it sends the dto with the users input to the ContactFactory and returns a contact. 
    /// That contact is added to the contact list. Then the list is saved to the file. The ContactListUpdated? event is then called, to make sure the using 
    /// classes update the list in the UI accordingly. 
    /// If everything works fine the method returns true, otherwise an exception message is written out and the method returns false.
    /// </summary>
    /// <param name="dto"></param>
    /// /// <returns>
    /// Returns <c>true</c> if the contact is successfully created and added to the list; otherwise, <c>false</c>.
    /// </returns>
    public bool CreateNewContact(ContactDto dto)
    {
        try
        {
            _contactList = _fileService.LoadListFromFile();

            var contact = _contactFactoryService.CreateContact(dto);
            _contactList.Add(contact);
            _fileService.SaveToFile(_contactList);
            ContactListUpdated?.Invoke(this, EventArgs.Empty);
            return true; 
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public event EventHandler? ContactListUpdated;

    /// <summary>
    /// The method first makes sure the saved list is updated. Then it checks if the list has any contacts in it. If not, it returns an empty list. 
    /// If the list isn't empty it is sorted (last name, then first name) and returned to the method calling it.
    /// </summary>
    /// <returns>
    /// A sorted list of contacts, or an empty list.
    /// </returns>

    public List<Contact> ViewAllContacts()
    {
        _contactList = _fileService.LoadListFromFile();

        if (_contactList.Count <= 0)
        {
            return [];
        }
        else
        {
            return _contactList.OrderBy(o => o.LastName).ThenBy(o => o.FirstName).ToList();
        }
    }

    /// <summary>
    /// The method takes a contact as a parameter. This Contact (except the id) has been edited by the user, converted into a dto, validated and 
    /// then converted back into a contact to be sent here. The method first makes sure the contact list is up to date. Then (in a try/catch statement, to avoid 
    /// the program breaking down if there are errors) it saves the index number of the contact in question (using its id to make sure to find the original) to a variable. 
    /// If the contact is not in the list, the method lets the user know and returns false. If the contact is in the list a new contact is created using a ContactDto
    /// with the members from the updated contact. Then the updated contact replaces the original one and is placed on the same index in the list. The list is saved to file, an
    /// event that the contactlist is updated fires. Lastly the method returns true. If something is wrong an exception message is presented and the method returns false. 
    /// </summary>
    /// <param name="contact"></param>
    /// <returns>
    /// <c>true</c> if the contact is updated successfully, otherwise <c>false</c>.
    /// </returns>

    public bool UpdateContact(Contact contact)
    {
        _contactList = _fileService.LoadListFromFile();

        try
        {
            int index = _contactList.FindIndex(c => c.Id == contact.Id);
            if (index == -1)
            {
                Debug.WriteLine($"Contact with Id {contact.Id} not found.");
                return false; // Contact not found
            }

            //^ChatGPT 4o

            Contact updatedContact = ContactFactory.CreateContact(
                new ContactDto(
                    contact.Id,
                    contact.FirstName,
                    contact.LastName,
                    contact.Email,
                    contact.PhoneNumber,
                    contact.StreetAddress,
                    contact.PostCode,
                    contact.City
                )
            );
            
            _contactList[index] = updatedContact;

            _fileService.SaveToFile(_contactList);
            ContactListUpdated?.Invoke(this, EventArgs.Empty);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    /// <summary>
    /// The method takes the contact the user wishes to delete as a parameter. It first makes sure the list is up to date. Then (in a try/catch statement) 
    /// finds the index of the contact and saves it to a variable. The contact is then deleted from the list, using the index to find it. After that, the list is saved to
    /// file nad the ContactListUpdated? event is raised. Then the method returns true. If something is wrong an exception message is presented and the method returns false. 
    /// </summary>
    /// <param name="contact"></param>
    /// <returns>
    /// <c>true</c> if contact is deleted successfully, otherwise <c>false</c>
    /// </returns>
    public bool DeleteContact(Contact contact)
    {
        _contactList = _fileService.LoadListFromFile();

        try
        {
            int index = _contactList.FindIndex(c => c.Id == contact.Id);
            _contactList.Remove(_contactList[index]);

            _fileService.SaveToFile(_contactList);
            ContactListUpdated?.Invoke(this, EventArgs.Empty);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}
