
using Business.DTOs;
using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace Business.Tests.Services;

public class ContactFactoryService_Tests
{
    private readonly IContactFactoryService _contactFactoryService;

    public ContactFactoryService_Tests()
    {
        _contactFactoryService = new ContactFactoryService();
    }

    [Fact]
    public void CreateContact_ShouldReturnContact_WhenProvidedWithContactDto()
    {
        // arrange
        ContactDto dto = new(null!, "Jasmin", "de Freitas", "jasmindefreitas@hotmail.com", "0761613824", "Rataryd Gärdet 1", "34194", "Ljungby");

        // act
        var result = _contactFactoryService.CreateContact(dto);

        // assert
        Assert.NotNull(result);
        Assert.IsType<Contact>(result);
        Assert.Equal("Jasmin", result.FirstName);
    }

    [Fact]
    public void CreateContactDto_ShouldReturnContactDto_WhenProvidedWithContact()
    {
        // arrange
        Contact contact = new()
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

        // act
        var result = _contactFactoryService.CreateContactDto(contact);

        // assert
        Assert.NotNull(result);
        Assert.IsType<ContactDto>(result);
        Assert.Equal("Jasmin", result.FirstName);
    }
}
