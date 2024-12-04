﻿
using System.Reflection.Metadata;
using Business.Models;
using Business.DTOs;
using Business.Factories;
using Business.Interfaces;

namespace Business.Services;

public class MenuService : IMenuService
{
    private readonly IFileService fileService;
    private readonly List<Contact> contactList;
    private readonly IContactService contactService;
    private readonly Helpers helper;


    public MenuService(IFileService fileService, IContactService contactService, Helpers helper)
    {
        this.fileService = fileService;
        this.contactService = contactService;
        this.helper = helper;
        //^using "this" is a suggestion from ChatGPT 4o

        contactList = fileService.LoadListFromFile();
    }

    public void MainMenu()
    {
        string choice = "";
        bool exit = false;
        

        do
        {
            Console.WriteLine("------ Welcome to your contact list! ------");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. View all contacts");
            Console.WriteLine("2. Create new conatct");
            //Console.WriteLine("3. Edit or delete contact");
            Console.WriteLine("3. Exit application");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Make your choice: ");
            choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    contactService.ViewAllContacts(contactList);
                    break;

                case "2":
                    contactService.CreateNewContact(contactList);
                    break;

                //case "3":
                //    EditOrDeleteContact(contactList);
                //    break;

                case "3":
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

    


    public void EditOrDeleteContact(List<Contact> contactList)
    {
        Console.WriteLine("--------- Edit or delete contact -----------");
        Console.WriteLine("Search contact: ");
        string search = Console.ReadLine()!.Trim();

        fileService.SaveToFile(contactList);   
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
            helper.Pause();
        }
        else
        {
            Console.Write("You didn't make a valid choice.");
            helper.Pause();
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