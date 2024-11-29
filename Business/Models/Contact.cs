﻿
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace Business.Models;

public class Contact
{
    public string Id { get; set; } = null!; 
    public string FirstName { get; set; } = null!; 
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string StreetAddress { get; set; } = null!; 
    public string PostCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public override string ToString()
    {
        return $"{FirstName} {LastName}, {Email}, {PhoneNumber}, {StreetAddress}, {PostCode}, {City}";
    }
}



