
using Business.Models; 
using Business.Interfaces;
using Business.Services;
using Moq;

namespace Business.Tests.Services;

public class FileService_Tests
{
    private readonly Mock<IFileService> _fileService;

    public FileService_Tests()
    {
        _fileService = new Mock<IFileService>();
    }

    [Fact]
    public void SaveToFile_ShouldReturnTrue_WhenFileIsSaved()
    {
        // arrange 
        List<Contact> contacts = new List<Contact>();
        _fileService.Setup(fs => fs.SaveToFile(contacts)).Returns(true);

        // act 
        var result = _fileService.Object.SaveToFile(contacts);

        // assert
        Assert.True(result);
    }

    [Fact]
    public void LoadListFromFile_ShouldReturnListOfOneContact_WhenFileIsLoaded()
    {
        // arrange 
        var expectedList = new List<Contact>();
        expectedList.Add(new Contact
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
        _fileService.Setup(fs => fs.LoadListFromFile()).Returns(expectedList);

        // act 
        var result = _fileService.Object.LoadListFromFile();

        // assert
        Assert.IsType<List<Contact>>(result);
        Assert.Equal(expectedList, result);
        Assert.Single(expectedList);
    }
}



// arrange 

// act 

// assert

//arrange _service.Do("meddelande")   Do = public void Do(string message)

// act setup x => x.Do

// hämtar upp var = message

// Assert
// Assert.Equal("meddelande", message)


