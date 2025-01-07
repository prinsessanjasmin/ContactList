using Presentation.Wpf_MainApp.ViewModels;
using Business.Configurations;
using Business.DTOs;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Presentation.Wpf_MainApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Presentation.Wpf_MainApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainView>();
                services.AddTransient<AddContactViewModel>();
                services.AddTransient<AddContactView>();
                services.AddTransient<EditOrRemoveContactViewModel>();
                services.AddTransient<EditOrRemoveContactView>();
                services.AddTransient<ViewAllContactsViewModel>();
                services.AddTransient<ViewAllContactsView>();
                services.AddTransient<ViewContactDetailsViewModel>();
                services.AddTransient<ViewContactDetailsView>();
                services.AddTransient<IFileService, FileService>();
                services.AddTransient<IContactService, ContactService>();
                services.AddTransient<IContactServiceCRUD, ContactService>();
                services.AddSingleton(new FileServiceConfig
                {
                    DirectoryPath = "C:\\Projects\\ContactList\\MainApp\\bin\\Debug\\net8.0\\Data",
                    FileName = "contactList.json"
                });
            }).Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<MainViewModel>();
        
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        
        mainWindow.Show();
    }
}
