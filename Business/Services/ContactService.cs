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
        string id = CreateUniqueId(); 
        try
        {
            var Contact = ContactFactory.Create(dto, id);
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
        Contact empty = ContactFactory.Create(emptyContact, id);

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
            Contact updatedContact = new()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                StreetAddress = contact.StreetAddress,
                PostCode = contact.PostCode,
                City = contact.City,
                
                DisplayName = contact.FirstName + " " + contact.LastName,
                Address = contact.StreetAddress + " " + contact.PostCode + " " + contact.City
            };
            
            int index = _contactList.FindIndex(c => c.Id == contact.Id);
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

    public string CreateUniqueId()
    {
        string newId = Guid.NewGuid().ToString();
        return newId;
    }
}
