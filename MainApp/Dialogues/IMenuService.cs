using Business.Models;
using Business.Services;

namespace Business.Interfaces;

public interface IMenuService
{
    void MainMenu();

    void CreateNewContactOption();
    bool ExitApp(bool exit); 
}
