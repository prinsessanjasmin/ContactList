namespace Business.Models;

/// <summary>
/// This model is used to store the data in the file. Two of the class members are just combinations of the other members, and are used to present the information using fewer lines of code. 
/// The ToString() method has a similar responsibility, used when presenting the list of contacts in the Console Application. 
/// </summary>
public partial class Contact 
{
    public string Id { get; set; } = null!; 
    public string FirstName { get; set; } = null!; 
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string StreetAddress { get; set; } = null!; 
    public string PostCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string DisplayName { get; set; } = null!; 
    public string Address { get; set; } = null!;


    public override string ToString() 
    {
        return $"Contact id: {Id}\nName: {DisplayName}\nEmail: {Email}\nPhone: {PhoneNumber}\nAddress: {Address}";
    }
}



