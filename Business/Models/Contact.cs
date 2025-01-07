namespace Business.Models;

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



