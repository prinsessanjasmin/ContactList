using Business.DTOs;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

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

    public bool CreateNewContact(ContactDto dto)
    {
        try
        {
            _contactList = _fileService.LoadListFromFile();

            var Contact = _contactFactoryService.CreateContact(dto);
            _contactList.Add(Contact);
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

    public List<Contact> ViewAllContacts()
    {
        _contactList = _fileService.LoadListFromFile();

        if (_contactList.Count <= 0)
        {
            return [];
        }
        else
        {
            return _contactList;
        }
    }

    public Contact FindContactById(string id)
    {
        _contactList = _fileService.LoadListFromFile();

        var emptyContact = ContactDto.CreateEmpty();
        Contact empty = _contactFactoryService.CreateContact(emptyContact);

        foreach (Contact contact in _contactList)
        {
            if (contact.Id == id)
            {
                ContactListUpdated?.Invoke(this, EventArgs.Empty);
                return contact;
            }
        }
        return empty;
    }

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
