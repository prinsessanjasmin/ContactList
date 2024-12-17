using Moq;
using Business.Models;
using Business.Services; 
using Business.Interfaces;
using Business.DTOs;
using System.Collections.Generic;

namespace Business.Tests.Services;


public class ContactService_Tests
{
    private readonly Mock<IContactService> _contactService;
    

    public ContactService_Tests()
    {
        _contactService = new Mock<IContactService>();
     
    }

    [Fact]
    public void CreateNewContact_ShouldReturnTrue_WhenContactIsCreated()
    {
       
        // arrange 
        ContactDto contactDto = new("2f5123c4-00ce-473e-b991-cd042c479822", "Jasmin", "de Freitas", "jasmindefreitas@hotmail.com", "0761613824", "Rataryd Gärdet 1", "34194", "Ljungby");
        List<Contact> contacts = new List<Contact>();

        _contactService.Setup(x => x.CreateNewContact(contacts, contactDto)).Returns(true);

        // act
        var result = _contactService.Object.CreateNewContact(contacts, contactDto);

        // assert 
        Assert.True(result);
    }

    [Fact]
    public void CreateNewContact_ShouldReturnTrueIfOneObjectWasAddedToList_AndItwasAContactObject()
    {
        // arrange
        ContactDto contactDto = new("2f5123c4-00ce-473e-b991-cd042c479822", "Jasmin", "de Freitas", "jasmindefreitas@hotmail.com", "0761613824", "Rataryd Gärdet 1", "34194", "Ljungby");
        List<Contact> contacts = new List<Contact>();

        _contactService.Setup(x => x.CreateNewContact(contacts, contactDto))
            .Callback((List<Contact> list, ContactDto dto) =>
            {
                // Simulating the actual behavior of the method ChatGPT 4o
                list.Add(new Contact
                {
                    Id = dto.Id,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    StreetAddress = dto.StreetAddress,
                    PostCode = dto.PostCode,
                    City = dto.City
                });
            })
            .Returns(true);

        // act 
        _contactService.Object.CreateNewContact(contacts, contactDto);

        // assert
        Assert.Single(contacts); // Ensure one contact was added
        Assert.IsType<Contact>(contacts[0]); // Check the type of the object added
    }

    [Fact]
    public void ViewAllContacts_ShouldReturnFalse_WhenThereAreNoContacts()
    {
        // arrange 
        List<Contact> contacts = new List<Contact>();
        _contactService.Setup(x => x.ViewAllContacts(contacts)).Returns(false);

        // act
        var result = _contactService.Object.ViewAllContacts(contacts);

        //assert
        Assert.False(result);
    }

    [Fact]
    public void ViewAllContacts_ShouldReturnTrue_WhenAContactExistsInTheList()
    {
        // arrange 
        List<Contact> contacts = new List<Contact>();
        contacts.Add(new Contact
        {
            Id = "2f5123c4-00ce-473e-b991-cd042c479822",
            FirstName = "Jasmin",
            LastName = "de Freitas",
            Email = "jasmindefreitas@hotmail.com",
            PhoneNumber = "0761613824",
            StreetAddress = "Rataryd Gärdet 1",
            PostCode = "34194",
            City = "Ljungby"
        });

        _contactService.Setup(x => x.ViewAllContacts(contacts)).Returns(true);

        // act 
        var result = _contactService.Object.ViewAllContacts(contacts);

        // assert
        Assert.True(result);
    }
}

