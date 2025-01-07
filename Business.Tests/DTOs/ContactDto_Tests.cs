using Business.DTOs;
using Moq; 

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

    [Fact]
    public void ValidateModel_ShouldDoSomething()
    {

    }
}
