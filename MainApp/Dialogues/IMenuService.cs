using Business.Models;
using Business.Services;

namespace Presentation.Console_MainApp.Dialogues;

public interface IMenuService
{
    void MainMenu();
    public void ViewAllContactsOption();
    bool CreateNewContactOption();
    bool ExitApp(bool exit);
}
