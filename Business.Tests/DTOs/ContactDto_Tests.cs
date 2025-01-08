using Business.DTOs;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Business.Tests.DTOs; 

public class ContactDto_Tests
{
    [Fact]
    public void ContactDto_ShouldReturnEmptyDto()
    {
        // arrange 
        ContactDto dto = new(null!, null!, null!, null!, null!, null!, null!, null!);

        // act 
        var result = ContactDto.CreateEmpty(); 

        // assert
        Assert.IsType<ContactDto>(result);
        Assert.Same(dto.FirstName, result.FirstName);
    }

    [Theory]
    [InlineData(null!, "", "    de Freitas", "jasmindefreitas@hotmail.com", "0761613824", "  Rataryd Gärdet 1", "34194", "Ljungby", true)] //Invalid
    [InlineData(null!, "Jasmin", "de Freitas", "jasmindefreitas@hotmail.com", "0761613824", "Rataryd Gärdet 1", "34194", "Ljungby", false)] //Valid
    [InlineData(null!, "J", "", "jasmindefreitas@hotmail.com", "0761613824", "Rataryd Gärdet 1", "34194", "Ljungby", true)] //Invalid
    [InlineData(null!, "Jasmin", "de Freitas", "jasmindefreitashotmail.com", "0761", "Rataryd Gärdet 1", "34194", "Ljungby", true)] //Invalid
    [InlineData(null!, "Jasmin", "de Freitas", " jasmindefreitas@hotmail.com", "0761613824", "", "34194", "Ljungby", true)] //Invalid
    [InlineData(null!, "Jasmin", "de Freitas", "jasmindefreitas@hotmail.com", "0761613824", "Rataryd Gärdet 1", "34", "Ljungby", true)] //Invalid
    [InlineData(null!, "Jasmin", "de Freitas", "jasmindefreitas@hotmail.com", "0761613824", "Rataryd Gärdet 1", "34194", "L", true)] //Invalid
    [InlineData(null!, "Jasmin", "de Freitas", "jasmindefreitas@hotmail.com", "0761613824", "Rataryd Gärdet 1", "34194", "", true)] //Invalid
    [InlineData(null!, "Jasmin     ", "     de Freitas", "jasmindefreitas@hotmail.com", "0761613824", "   Rataryd Gärdet 1", "34194", "Ljungby", false)] //Valid
    [InlineData(null!, "", "", "", "", "", "", "", true)] //Invalid
    public void ValidateModel_ShouldDisplayErrorsWhenFaultyValuesAreAdded(string id, string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postCode, string city, bool shouldHaveErrors)
    {
        // arrange
        ContactDto dto = new(id, firstName, lastName, email, phoneNumber, streetAddress, postCode, city);
        
        // act 
        dto.ValidateModel();

        //assert
        Assert.Equal(shouldHaveErrors, dto.HasErrors);
        if (shouldHaveErrors)
        {
            var errors = dto.GetErrors(null).Cast<ValidationResult>().ToList();
            Assert.NotEmpty(errors);
            foreach ( var error in errors )
            {
                Console.WriteLine(error.ErrorMessage);
            }
        } 
        else
        {
            Assert.Empty(dto.GetErrors(null));
        }
        //^Got help here from ChatGPT 4o
    }
}
