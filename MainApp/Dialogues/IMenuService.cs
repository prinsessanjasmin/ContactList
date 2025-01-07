using Business.Models;
using Business.Services;

namespace Presentation.Console_MainApp.Dialogues;

public interface IMenuService
{
    void MainMenu();
    void ViewAllContactsOption();
    bool CreateNewContactOption();
    bool ExitApp(bool exit);
    string PromptAndValidate(string prompt, string propertyName);
    void Pause();
}
