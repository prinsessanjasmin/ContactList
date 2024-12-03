using Business.Models;
using Business.Services;

namespace Business.Interfaces;

public interface IMenuService
{
    void MainMenu();

    bool ExitApp(bool exit); 

}
