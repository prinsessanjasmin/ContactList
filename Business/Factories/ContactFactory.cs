﻿using Business.Models;
using Business.DTOs;
using Business.Services;

namespace Business.Factories;

internal class ContactFactory
{
    public static Contact Create(ContactDto dto)
    {
        return new Contact
        {
            Id = Helpers.CreateUniqueId(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            StreetAddress = dto.StreetAddress,
            PostCode = dto.PostCode,
            City = dto.City
        };
    }
}
