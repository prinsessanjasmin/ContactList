using Business.Configurations;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Services;

public class FileService : IFileService
{
    private readonly string _directoryPath; 
    private readonly string _fileName;

    public FileService(FileServiceConfig config)
    {
        _directoryPath = config.DirectoryPath;
        _fileName = Path.Combine (_directoryPath, config.FileName);
    }

    public bool SaveToFile(List<Contact> contacts)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            var json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(_fileName, json);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
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
