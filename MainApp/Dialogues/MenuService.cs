
using System.Reflection.Metadata;
using Business.Models;
using Business.DTOs;
using Business.Interfaces;
using Business.Services;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Console_MainApp.Dialogues;

public class MenuService(IContactService contactService) : IMenuService
{
    private readonly IContactService _contactService = contactService;

    public void MainMenu()
    {
        string choice;
        bool exit = false;

        do
        {
            Console.Clear();
            Console.WriteLine("------ Welcome to the Address Book! ------");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. View all contacts");
            Console.WriteLine("2. Create new conatct");
            Console.WriteLine("3. Exit application");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Make your choice: ");
            choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    ViewAllContactsOption();
                    Pause();
                    break;

                case "2":
                    CreateNewContactOption();
                    Pause();
                    break;
                case "3":
                    exit = ExitApp(exit);
                    break;

                default:
                    Console.WriteLine("You must make a choice!");
                    Pause();
                    break;
            }
        } while (!exit);

        Console.WriteLine("Thanks for using the Address Book. Have a good day!");
        Console.ReadKey();
    }

    public void ViewAllContactsOption()
    {
        List<Contact> contactList = _contactService.ViewAllContacts();
        if (contactList.Count < 0)
        {
            Console.WriteLine("There are no contacts in this list.");
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
        }
    }
    public bool CreateNewContactOption()
    {
        try
        {
            ContactDto dto = ContactDto.CreateEmpty();

            Console.WriteLine("------------- Add new contact --------------");
            dto.FirstName = PromptAndValidate("First name: ", nameof(ContactDto.FirstName));
            dto.LastName = PromptAndValidate("Last name: ", nameof(ContactDto.LastName));
            dto.Email = PromptAndValidate("Email: ", nameof(ContactDto.Email));
            dto.PhoneNumber = PromptAndValidate("Phone number: ", nameof(ContactDto.PhoneNumber));
            dto.StreetAddress = PromptAndValidate("Street Address: ", nameof(ContactDto.StreetAddress));
            dto.PostCode = PromptAndValidate("Post code: ", nameof(ContactDto.PostCode));
            dto.City = PromptAndValidate("City: ", nameof(ContactDto.City));

            var result = _contactService.CreateNewContact(dto);
            if (result)
            {
                Console.WriteLine($"The contact details were added.");
                return true;
            } else
            {
                Console.WriteLine("Something went wrong.");
                return false; 
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }

    }

    public bool ExitApp(bool exit)
    {
        Console.Write("Are you sure you want to exit the application? Y/N: ");
        string confirm = Console.ReadLine()!;
        if (confirm.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            exit = true;
        }
        else if (confirm.Equals("n", StringComparison.OrdinalIgnoreCase))
        {
            Pause();
        }
        else
        {
            Console.Write("You didn't make a valid choice.");
            Pause();
        }
        return exit;
    }

    public string PromptAndValidate(string prompt, string propertyName)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine() ?? string.Empty;

            var results = new List<ValidationResult>();
            var context = new ValidationContext(ContactDto.CreateEmpty()) { MemberName = propertyName };

            if (Validator.TryValidateProperty(input, context, results))
            {
                return input;
            }
            Console.WriteLine($"{results[0].ErrorMessage} Please try again.");
        }
    }

    public void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("Press any key to return to the main menu");
        Console.ReadKey();
    }
}