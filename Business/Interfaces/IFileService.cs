using Business.Models;
using Business.Services;

namespace Business.Interfaces;

public interface IFileService
{
    bool SaveToFile(List<Contact> contacts); 
    List<Contact> LoadListFromFile();
}
