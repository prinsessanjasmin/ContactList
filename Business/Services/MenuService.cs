
using System.Reflection.Metadata;
using Business.Models;
using Business.DTOs;
using Business.Factories;

namespace Business.Services;

public class MenuService
{
    private static readonly FileService fileService = new FileService();
    private static List<Contact> contactList = fileService.LoadListFromFile();

    public static void MainMenu()
    {
        string choice = "";
        bool exit = false;
        

        do
        {
            Console.WriteLine("------ Welcome to your contact list! ------");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. View all contacts");
            Console.WriteLine("2. Create new conatct");
            Console.WriteLine("3. Edit or delete contact");
            Console.WriteLine("4. Exit application.");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Make your choice: ");
            choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    ViewAllContacts(contactList);
                    break;

                case "2":
                    CreateNewContact(contactList);
                    break;

                case "3":
                    EditOrDeleteContact(contactList);
                    break;

                case "4":
                    exit = ExitApp(exit);
                    break;

                default:
                    Console.WriteLine("You must make a choice!");
                    break;
            
            } Console.Clear(); 
        
        } while (!exit);
        
        Console.WriteLine("Thanks for using the contact list. Have a good day!");
        Console.ReadKey();

    }

    internal static void ViewAllContacts(List<Contact> contactList)
    {
        Console.WriteLine("-------------- Your contacts: --------------");
        Console.WriteLine();

        foreach (Contact contact in contactList) {
            Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine($"Phone: {contact.PhoneNumber}");
            Console.WriteLine($"Address: {contact.StreetAddress}, {contact.PostCode} {contact.City}");

            Console.WriteLine();
        }

        Helpers.Pause();
    }

    internal static void CreateNewContact(List<Contact> contactList)
    {
        Console.WriteLine("------------- Add new contact --------------");
        Console.Write("First name: "); 
        string firstName = Console.ReadLine()!.Trim();
        string vFirstName = Helpers.ValidateInput(firstName, "name");

        Console.Write("Last name: ");
        string lastName = Console.ReadLine()!.Trim();
        string vLastName = Helpers.ValidateInput(lastName, "last name");

        Console.Write("Email: ");
        string email = Console.ReadLine()!.Trim();
        string vEmail = Helpers.ValidateEmail(email);

        Console.Write("Phone number: ");
        string phoneNumber = Console.ReadLine()!.Trim();
        string vPhoneNumber = Helpers.ValidatePhone(phoneNumber);

        Console.Write("Street address: ");
        string streetAddress = Console.ReadLine()!.Trim();
        string vStreetAddress = Helpers.ValidateInput(streetAddress, "street address");

        Console.Write("Post code (numbers): ");
        string postCode = Console.ReadLine()!.Trim();
        string vPostCode = Helpers.ValidatePostCode(postCode);

        Console.Write("City: ");
        string city = Console.ReadLine()!.Trim();
        string vCity = Helpers.ValidateInput(city, "city");

        var Contact = ContactFactory.Create(vFirstName, vLastName, vEmail, vPhoneNumber, vStreetAddress, vPostCode, vCity);
        contactList.Add(Contact);
        fileService.SaveToFile(contactList);
        Console.WriteLine($"{vFirstName}'s contact details were added.");
        Helpers.Pause();

    }

    
    internal static void EditOrDeleteContact(List<Contact> contactList)
    {
        Console.WriteLine("--------- Edit or delete contact -----------");
        Console.WriteLine("Search contact: ");
        string search = Console.ReadLine()!.Trim();

        

        fileService.SaveToFile(contactList);   
    }
 

    internal static bool ExitApp(bool exit)
    {
        Console.Write("Are you sure you want to exit the application? Y/N: ");
        string confirm = Console.ReadLine()!;
        if (confirm.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            exit = true;
        }
        else if (confirm.Equals("n", StringComparison.OrdinalIgnoreCase))
        {
            Helpers.Pause();
        }
        else
        {
            Console.Write("You didn't make a valid choice.");
            Helpers.Pause();
        }
        return exit;
    }

}


/*
 Introduktion
I denna inlämningsuppgift ska du bygga en applikation med hjälp av C#. Applikationen ska uppfylla de kriterier som specificeras 
här nedan. För godkänt räcker det med att applikationen är en konsolapplikation, medan för väl godkänt krävs även att du skapar 
en till applikation som använder sig av ett GUI (Graphical User Interface) såsom MAUI, MAUI Blazor eller WPF.

Om du använder dig av kodstycken som är genererade av AI, såsom Chat GPT, måste detta framgå för att det inte ska räknas som 
plagiat. Du ska då även kommentera koden och förklara vad den gör. Detta görs genom att använda vanliga kommentarer eller, 
för längre stycken, med en sammanfattande summary-kommentar.

Instruktioner på vad som ska göras
Applikationen du ska skapa ska vara en kontaktlista där det ska vara möjligt att lägga till och se kontakter.En kontakt ska ha 
följande information: förnamn, efternamn, e-postadress, telefonnummer, gatuadress, postnummer och ort.Varje kontakt ska även 
få ett autogenererat id i form av en GUID.

För godkänt krävs följande:

En applikation som är byggd som en konsolapplikation med följande:

Ett menyalternativ som listar alla kontakter.
Ett menyalternativ som skapar en kontakt.
Möjlighet att spara kontakter till en .json-fil.
Möjlighet att läsa in kontakter från en.json-fil när applikationen startar och när listan uppdateras.
Tillämpning av S i SOLID.
Enhetstester (utan mock) på de metoder som kan testas utan att använda mock.


För väl godkänt krävs följande:

Att du gör en konsolapplikation (se G-kraven).

Att du gör en till applikation, som använder sig av ett GUI, med följande:

En sida som listar alla kontakter.
En sida där man kan skapa en kontakt.
En sida där man kan redigera en kontakt med möjlighet att uppdatera och ta bort kontakten.
Möjlighet att spara kontakter till en .json-fil.
Möjlighet att läsa in kontakter från en.json-fil när applikationen startar och när listan uppdateras.
Användning av Dependency Injection.
Användning av Klassbibliotek (Class Library).
Tillämpning av flera olika Design Patterns: SOLID, Service Pattern och Factory Pattern.
Enhetstester (med mock där det behövs) för alla metoder som används.

 */