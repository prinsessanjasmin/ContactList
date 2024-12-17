using Business.DTOs;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService(IFileService fileService, Helpers helper) : IContactService
{
    private readonly IFileService _fileService = fileService;
    private readonly Helpers _helper = helper;

    public bool CreateNewContact(List<Contact> contactList, ContactDto dto)
    {
        try
        {
            var Contact = ContactFactory.Create(dto);
            contactList.Add(Contact);
            _fileService.SaveToFile(contactList);
            Console.WriteLine($"{Contact.FirstName}'s contact details were added.");
            _helper.Pause();
            return true; 
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool ViewAllContacts(List<Contact> contactList)
    {
        if (contactList.Count <= 0)
        {
            Console.WriteLine("There are no contacts in this list.");
            _helper.Pause();
            return false;
        }
        else
        {
            Console.WriteLine("-------------- Your contacts: --------------");
            Console.WriteLine();
            
            foreach (Contact contact in contactList)
            {
                string contactString = contact.ToString();
                Console.WriteLine(contactString); 
                Console.WriteLine();
            }
            _helper.Pause();
            return true; 
        }
    }
}
