using Business.Models;
using Business.Services;

namespace Presentation.Console_MainApp.Dialogues;

public interface IMenuService
{
    void MainMenu();
    bool CreateNewContactOption();
    bool ExitApp(bool exit);
}
