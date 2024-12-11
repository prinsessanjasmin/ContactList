using Moq;
using Business.Models;
using Business.Services; 
using Business.Interfaces;
using Business.DTOs;

namespace Business.Tests.Services;


public class ContactService_Tests
{
    private readonly IContactService _contactService;
    private readonly Mock<IContactFactoryService> _contactFactoryServiceMock;
    
    public ContactService_Tests()
    {
        // Initialize the mock
        _contactFactoryServiceMock = new Mock<IContactFactoryService>();

        // Inject the mock into the service under test
        _contactService = new ContactService(_contactFactoryServiceMock.Object);
    }

    [Fact]
    public void CreateNewContact_ShouldReturnNewContactObject()
    {
        // arrange 
        ContactDto contactDto = new ContactDto("Jasmin", "de Freitas", "jasmindefreitas@hotmail.com", "0761613824", "Rataryd Gärdet 1", "34194", "Ljungby");
        List<Contact> contacts = new List<Contact>();
        Contact expectedContact = new() { FirstName = "Jasmin", LastName = "de Freitas", Email = "jasmindefreitas@hotmail.com", PhoneNumber = "0761613824", StreetAddress = "Rataryd Gärdet 1", PostCode = "34194", City = "Ljungby" };

        _contactFactoryServiceMock
            .Setup(cfs => cfs.Create(contactDto))
            .Returns(expectedContact);
        // act
        var result = _contactService.CreateNewContact(contacts, contactDto);

        // assert 
        Assert.IsType<Contact>(result);
        Assert.Equal(expectedContact.FirstName, result.FirstName); 
        Assert.Equal(expectedContact.PhoneNumber, result.PhoneNumber);
        Assert.Equal(expectedContact.Email, result.Email);
    }    
}


