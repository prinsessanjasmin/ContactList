using Business.DTOs;
using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService(IFileService fileService, Helpers helper) : IContactService
{
    private readonly IFileService fileService = fileService;
    private readonly Helpers helper = helper;

    public void CreateNewContact(List<Contact> contactList, ContactDto dto)
    {
        var Contact = ContactFactory.Create(dto);
        contactList.Add(Contact);
        fileService.SaveToFile(contactList);
        Console.WriteLine($"{Contact.FirstName}'s contact details were added.");
        helper.Pause();
    }

    public void ViewAllContacts(List<Contact> contactList)
    {
        if (contactList.Count < 1)
        {
            Console.WriteLine("There are no contacts in this list."); 
            helper.Pause();
        }
        else
        {
            Console.WriteLine("-------------- Your contacts: --------------");
            Console.WriteLine();
            
            foreach (Contact contact in contactList)
            {
                Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                Console.WriteLine($"Address: {contact.StreetAddress}, {contact.PostCode} {contact.City}");
                Console.WriteLine();
            }
            helper.Pause();
        }
    }
}
