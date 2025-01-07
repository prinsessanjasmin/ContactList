using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs;

public partial class ContactDto : ObservableValidator
{
    public string Id {  get; set; }

    [ObservableProperty]
    [Required(ErrorMessage = "First name is required.")]
    [MinLength(2, ErrorMessage = "First name must contain at least two characters.")]
    private string _firstName;

    partial void OnFirstNameChanged(string value)
    {
        ValidateProperty(value, nameof(FirstName));
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Last name is required.")]
    [MinLength(2, ErrorMessage = "Last name must contain at least two characters.")]
    private string _lastName;

    partial void OnLastNameChanged(string value)
    {
        ValidateProperty(value, nameof(LastName));
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
    //^ Regex created by ChatGPT 4o to match patterns for email addresses
    private string _email;

    partial void OnEmailChanged(string value)
    {
        ValidateProperty(value, nameof(Email));
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^(\+46|0)(\s?\d{2,4})\s?\d{2,3}\s?\d{2,3}$", ErrorMessage = "Please enter a valid Swedish phone number.")]
    private string _phoneNumber;

    partial void OnPhoneNumberChanged(string value)
    {
        ValidateProperty(value, nameof(PhoneNumber));
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Street address is required.")]
    [MinLength(2, ErrorMessage = "Street address must contain at least two characters.")]
    private string _streetAddress;

    partial void OnStreetAddressChanged(string value)
    {
        ValidateProperty(value, nameof(StreetAddress));
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Post code is required.")]
    [RegularExpression(@"^\d{3}\s?\d{2}$", ErrorMessage = "Please enter a valid post code, consisting of 5 digits.")]
    private string _postCode;

    partial void OnPostCodeChanged(string value)
    {
        ValidateProperty(value, nameof(PostCode));
    }

    [ObservableProperty]
    [Required(ErrorMessage = "City is required.")]
    [MinLength(2, ErrorMessage = "City must contain at least two characters.")]
    private string _city;

    partial void OnCityChanged(string value)
    {
        ValidateProperty(value, nameof(City));
    }

    public ContactDto(string id, string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postCode, string city)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        StreetAddress = streetAddress;
        PostCode = postCode;
        City = city;
    }

    public void ValidateModel()
    {
        ClearErrors(); // Clear previous errors
        ValidateAllProperties(); // Validate all properties based on annotations
    }
    //^ChatGPT 4o

    public static ContactDto CreateEmpty()
    {
        return new ContactDto(null!, null!, null!, null!, null!, null!, null!, null!);
    }
}
