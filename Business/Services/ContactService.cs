using Business.DTOs;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService(IFileService fileService, Helpers helper) : IContactService
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
}
