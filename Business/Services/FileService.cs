

using Business.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Services;

internal class FileService
{
    private readonly string _directoryPath; 
    private readonly string _fileName;

    public FileService(string directoryPath = "Data", string fileName = "contactList.json")
    {
        _directoryPath = directoryPath;
        _fileName = Path.Combine (_directoryPath, fileName);
    }

    public void SaveToFile(List<Contact> contacts)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            var json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(_fileName, json);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public List<Contact> LoadListFromFile()
    {
        try
        {
            if (!File.Exists(_fileName))
            {
                return [];
            }

            var json = File.ReadAllText(_fileName);
            var contacts = JsonSerializer.Deserialize<List<Contact>>(json);
            return contacts ?? []; 
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }
    }
}
