using Business.DTOs;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService(IFileService fileService, Helpers helper) : IContactService, IContactServiceCRUD
{
    private readonly List<Contact> _contactList = fileService.LoadListFromFile();
    private readonly IFileService _fileService = fileService;
    private readonly Helpers _helper = helper;

    public bool CreateNewContact(ContactDto dto)
    {
        try
        {
            var Contact = ContactFactory.Create(dto);
            _contactList.Add(Contact);
            _fileService.SaveToFile(_contactList);
            Console.WriteLine($"{Contact.FirstName}'s contact details were added.");
            return true; 
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public List<Contact> ViewAllContacts()
    {
        if (_contactList.Count <= 0)
        {
            Console.WriteLine("There are no contacts in this list.");
            return [];
        }
        else
        {
            Console.WriteLine("-------------- Your contacts: --------------");
            Console.WriteLine();

            foreach (Contact contact in _contactList)
            {
                string contactString = contact.ToString();
                Console.WriteLine(contactString);
                Console.WriteLine();
            }
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
                City = contact.City
            };
            updatedContact.DisplayName = updatedContact.FirstName + " " + updatedContact.LastName;
            updatedContact.Address = updatedContact.StreetAddress + " " + updatedContact.PostCode + " " + updatedContact.City; 
            
            int index = _contactList.IndexOf(contact);
            _contactList.Remove(_contactList[index]); 
            _contactList.Add(updatedContact);   

            _fileService.SaveToFile(_contactList);
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
            int index = _contactList.IndexOf(contact);
            _contactList.Remove(_contactList[index]);

            _fileService.SaveToFile(_contactList);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    

    
}
