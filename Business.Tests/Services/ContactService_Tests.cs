using Moq;
using Business.Models;
using Business.Services; 
using Business.Interfaces;
using Business.DTOs;
using System.Collections.Generic;
using NuGet.Frameworks;

namespace Business.Tests.Services;

public class ContactService_Tests
{
    private readonly IContactServiceCRUD _contactServiceCRUD;
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly Mock<IContactFactoryService> _contactFactoryServiceMock;

    public ContactService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _contactFactoryServiceMock = new Mock<IContactFactoryService>();
        _contactServiceCRUD = new ContactService(_fileServiceMock.Object, _contactFactoryServiceMock.Object);
    }

    [Fact]
    public void CreateNewContact_ShouldReturnTrue_WhenContactIsCreated()
    {

        // arrange         
        ContactDto dto = new(null!, "Jasmin", "de Freitas", "jasmindefreitas@hotmail.com", "0761613824", "Rataryd Gärdet 1", "34194", "Ljungby");
        Contact expectedContact = new()
        {
            Id = "2f5123c4-00ce-473e-b991-cd042c479822",
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            StreetAddress = dto.StreetAddress,
            PostCode = dto.PostCode,
            City = dto.City
        };

        List<Contact> contacts = new List<Contact>();

        _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns(contacts);

        _contactFactoryServiceMock
            .Setup(cfs => cfs.CreateContact(It.IsAny<ContactDto>()))
            .Returns(expectedContact);

        _fileServiceMock
            .Setup(fs => fs.SaveToFile(It.IsAny<List<Contact>>()))
            .Returns(true);

        // act
        var result = _contactServiceCRUD.CreateNewContact(dto);

        // Assert
        Assert.True(result);
        Assert.Single(contacts);
        Assert.Equal(expectedContact, contacts[0]);
        _fileServiceMock.Verify(fs => fs.SaveToFile(It.IsAny<List<Contact>>()), Times.Once);
    }

    [Fact]
    public void ViewAllContacts_ShouldReturnListOfOneContact()
    {
        // arrange
        List<Contact> contacts = new List<Contact>();
        Contact expectedContact = new()
        {
            Id = "2f5123c4-00ce-473e-b991-cd042c479822",
            FirstName = "Jasmin",
            LastName = "de Freitas",
            Email = "jasmindefreitas@hotmail.com",
            PhoneNumber = "0761613824",
            StreetAddress = "Rataryd Gärdet 1",
            PostCode = "34194",
            City = "Ljungby"
        };
        contacts.Add(expectedContact);
        _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns(contacts);

        //act
        var result = _contactServiceCRUD.ViewAllContacts();

        //assert
        Assert.IsType<List<Contact>>(result);
        Assert.Single(result);
    }

    [Fact]
    public void FindContactById_ShouldReturnContactWithCorrectId()
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
        contacts.Add(new Contact
        {
            Id = "b4ea757d-b297-49e2-b0a9-296f634e47ee",
            FirstName = "Robin",
            LastName = "Pafumi Dahlin",
            Email = "robin.pafumi@gmail.com",
            PhoneNumber = "0705157028",
            StreetAddress = "Rataryd Gärdet 1",
            PostCode = "34194",
            City = "Ljungby"
        });


        _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns(contacts);

        // act 
        var result = _contactServiceCRUD.FindContactById("b4ea757d-b297-49e2-b0a9-296f634e47ee");

        // assert
        Assert.IsType<Contact>(result);
        Assert.Equal("Robin", result.FirstName);
    }

    [Fact]
    public void UpdateContact_ShouldReturnTrueIfContactUpdatesSuccessfylly()
    {
        // arrange 
        List<Contact> contacts = new List<Contact>();
        Contact originalContact = new()
        {
            Id = "2f5123c4-00ce-473e-b991-cd042c479822",
            FirstName = "Jasmin",
            LastName = "de Freitas",
            Email = "jasmindefreitas@hotmail.com",
            PhoneNumber = "0761613824",
            StreetAddress = "Rataryd Gärdet 1",
            PostCode = "34194",
            City = "Ljungby"
        };
        Contact updatedContact = new()
        {
            Id = "2f5123c4-00ce-473e-b991-cd042c479822",
            FirstName = "Jasmin Anna",
            LastName = "de Freitas",
            Email = "jasmindefreitas@hotmail.com",
            PhoneNumber = "0761613824",
            StreetAddress = "Rataryd Gärdet 1",
            PostCode = "34194",
            City = "Ljungby"
        };
        contacts.Add(originalContact);

        _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns(contacts);
        _fileServiceMock
            .Setup(fs => fs.SaveToFile(It.IsAny<List<Contact>>()))
            .Returns(true);

        //act
        var result = _contactServiceCRUD.UpdateContact(updatedContact);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteContact_ShouldReturnTrueIfContactIsDeleted()
    {
        //arrange
        List<Contact> contacts = new List<Contact>();
        Contact contactToDelete = new()
        {
            Id = "2f5123c4-00ce-473e-b991-cd042c479822",
            FirstName = "Jasmin",
            LastName = "de Freitas",
            Email = "jasmindefreitas@hotmail.com",
            PhoneNumber = "0761613824",
            StreetAddress = "Rataryd Gärdet 1",
            PostCode = "34194",
            City = "Ljungby"
        };
        contacts.Add(contactToDelete);
        contacts.Add(new Contact
        {
            Id = "b4ea757d-b297-49e2-b0a9-296f634e47ee",
            FirstName = "Robin",
            LastName = "Pafumi Dahlin",
            Email = "robin.pafumi@gmail.com",
            PhoneNumber = "0705157028",
            StreetAddress = "Rataryd Gärdet 1",
            PostCode = "34194",
            City = "Ljungby"
        });
        _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns(contacts);
        _fileServiceMock
            .Setup(fs => fs.SaveToFile(It.IsAny<List<Contact>>()))
            .Returns(true);

        //act
        var result = _contactServiceCRUD.DeleteContact(contactToDelete);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteContact_ShouldReturnListWithOneContact_AfterOneIsDeleted()
    {
        //arrange
        List<Contact> contacts = new List<Contact>();
        Contact contactToDelete = new()
        {
            Id = "2f5123c4-00ce-473e-b991-cd042c479822",
            FirstName = "Jasmin",
            LastName = "de Freitas",
            Email = "jasmindefreitas@hotmail.com",
            PhoneNumber = "0761613824",
            StreetAddress = "Rataryd Gärdet 1",
            PostCode = "34194",
            City = "Ljungby"
        };
        contacts.Add(new Contact
        {
            Id = "b4ea757d-b297-49e2-b0a9-296f634e47ee",
            FirstName = "Robin",
            LastName = "Pafumi Dahlin",
            Email = "robin.pafumi@gmail.com",
            PhoneNumber = "0705157028",
            StreetAddress = "Rataryd Gärdet 1",
            PostCode = "34194",
            City = "Ljungby"
        });
        contacts.Add(contactToDelete);
        _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns(contacts);
        _fileServiceMock
            .Setup(fs => fs.SaveToFile(It.IsAny<List<Contact>>()))
            .Returns(true);

        //act
        var delete = _contactServiceCRUD.DeleteContact(contactToDelete);
        var result = contacts.Count;

        //assert
        Assert.Equal(1, result);
    }
}
