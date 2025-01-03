﻿namespace Presentation.Console_MainApp.Dialogues
{
    public interface IHelpers
    {
        void Pause();
        string ValidateEmail(string email);
        string ValidateInput(string input, string expected);
        string ValidatePhone(string phoneNumber);
        string ValidatePostCode(string postCode);
    }
}