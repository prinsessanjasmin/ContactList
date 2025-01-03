﻿using System.Text.RegularExpressions;

namespace Presentation.Console_MainApp.Dialogues;

internal class Helpers : IHelpers
{
    public void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("Press any key to return to the main menu");
        Console.ReadKey();
    }

    public string ValidateInput(string input, string expected)
    {
        while (string.IsNullOrEmpty(input) || (input.Length < 2))
        {
            Console.WriteLine($"The contact's {expected} must be at least 2 characters long! Please try again");
            input = Console.ReadLine()!.Trim();
        }

        return input;
    }

    public string ValidateEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        //^ Regex created by ChatGPT 4o to match patterns for email addresses

        while (!Regex.IsMatch(email, emailPattern))
        {
            Console.WriteLine("The email adress you entered is not in the correct format. Please try again: ");
            email = Console.ReadLine()!.Trim();
        }
        return email;
    }

    public string ValidatePhone(string phoneNumber)
    {
        string phonePattern = @"^(\+46|0)(\s?\d{2,4})\s?\d{2,3}\s?\d{2,3}$";
        //^ Regex created by ChatGPT 4o to match common patterns of Swedish telephone numbers 

        while (!Regex.IsMatch(phoneNumber, phonePattern))
        {
            Console.WriteLine("You need to enter a valid Swedish phone number. Please try again: ");
            phoneNumber = Console.ReadLine()!.Trim();
        }
        return phoneNumber;
    }

    public string ValidatePostCode(string postCode)
    {
        string postCodePattern = @"^\d{3}\s?\d{2}$";
        //^ Regex created by ChatGPT 4o to match common patterns of Swedish post codes

        while (!Regex.IsMatch(postCode, postCodePattern))
        {
            Console.WriteLine($"The post code must be in the form of 5 digits! Please try again: ");
            postCode = Console.ReadLine()!.Trim();
        }
        return postCode;
    }
}
