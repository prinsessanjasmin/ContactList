using Business.DTOs;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation.Wpf_MainApp.ViewModels;

public partial class AddContactViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactServiceCRUD _contactService;

    [ObservableProperty]
    private ContactDto _newContact;

    public AddContactViewModel(IServiceProvider serviceProvider, IContactServiceCRUD contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        NewContact = ContactDto.CreateEmpty();
        CurrentViewModel = this;
    }

    [ObservableProperty]
    private ObservableObject _currentViewModel;

    [ObservableProperty]
    private string _title = "Add New Contact";

    [RelayCommand]
    private void Save()
    {
        var result = _contactService.CreateNewContact(NewContact);

        if (result) 
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ViewAllContactsViewModel>();
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
    }
}
