using Business.DTOs;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService(IFileService fileService): IContactService, IContactServiceCRUD
{
    private readonly List<Contact> _contactList = fileService.LoadListFromFile();
    private readonly IFileService _fileService = fileService;

    public bool CreateNewContact(ContactDto dto)
    {
        try
        {
            var Contact = ContactFactory.CreateContact(dto);
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
        if (_contactList.Count <= 0)
        {
            return [];
        }
        else
        {
            return _contactList;
        }
    }

    public IEnumerable<Contact> FindContact(string searchWord)
    {
        IEnumerable<Contact> searchResult = null!;

        //if (searchWord == null)
        //{
        //    return searchResult;
        //}
        //else if (_contactList.FirstOrDefault(i => i.Id == searchWord)
        
        return searchResult; 
    }

    public Contact FindContactById(string id) 
    {
        var emptyContact = ContactDto.CreateEmpty();
        Contact empty = ContactFactory.CreateContact(emptyContact);

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
