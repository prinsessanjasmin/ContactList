
using Business.Interfaces;
using Business.Services;
using Moq;
using System.Text.RegularExpressions;

namespace Business.Tests.Services;

public class Helpers_Tests
{
    private readonly Mock<IHelpers> _helpersMock;
    private readonly IHelpers _helper;
    

    public Helpers_Tests()
    {
        _helpersMock = new Mock<IHelpers>();
        _helper = new Helpers(); 
    }



// arrange 

// act 

// assert

//arrange _service.Do("meddelande")   Do = public void Do(string message)

// act setup x => x.Do

// hämtar upp var = message

// Assert
// Assert.Equal("meddelande", message)
