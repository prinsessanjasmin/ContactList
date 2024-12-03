

using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService : IContactService
{
    private readonly FileService fileService;
    private readonly List<Contact> contactList;
    private readonly Helpers helper;

    public ContactService()
    {
        fileService = new FileService();
        contactList = fileService.LoadListFromFile();
        helper = new Helpers();
    }

    public void CreateNewContact(List<Contact> contactList)
    {
        Console.WriteLine("------------- Add new contact --------------");
        Console.Write("First name: ");
        string firstName = Console.ReadLine()!.Trim();
        string vFirstName = helper.ValidateInput(firstName, "name");

        Console.Write("Last name: ");
        string lastName = Console.ReadLine()!.Trim();
        string vLastName = helper.ValidateInput(lastName, "last name");

        Console.Write("Email: ");
        string email = Console.ReadLine()!.Trim();
        string vEmail = helper.ValidateEmail(email);

        Console.Write("Phone number: ");
        string phoneNumber = Console.ReadLine()!.Trim();
        string vPhoneNumber = helper.ValidatePhone(phoneNumber);

        Console.Write("Street address: ");
        string streetAddress = Console.ReadLine()!.Trim();
        string vStreetAddress = helper.ValidateInput(streetAddress, "street address");

        Console.Write("Post code (numbers): ");
        string postCode = Console.ReadLine()!.Trim();
        string vPostCode = helper.ValidatePostCode(postCode);

        Console.Write("City: ");
        string city = Console.ReadLine()!.Trim();
        string vCity = helper.ValidateInput(city, "city");

        var Contact = ContactFactory.Create(vFirstName, vLastName, vEmail, vPhoneNumber, vStreetAddress, vPostCode, vCity);
        contactList.Add(Contact);
        fileService.SaveToFile(contactList);
        Console.WriteLine($"{vFirstName}'s contact details were added.");
        helper.Pause();

    }

    public void ViewAllContacts(List<Contact> contactList)
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
