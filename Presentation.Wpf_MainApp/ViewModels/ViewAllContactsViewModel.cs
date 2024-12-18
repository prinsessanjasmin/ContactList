using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Business.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using Business.Interfaces;

namespace Presentation.Wpf_MainApp.ViewModels;

public partial class ViewAllContactsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;
    
    [ObservableProperty]
    private ObservableObject _currentViewModel;

    [ObservableProperty]
    private string _title = "Your Contacts";

    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];

    public ViewAllContactsViewModel(IServiceProvider serviceProvider, IContactService contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        CurrentViewModel = this; 
        Contacts = new ObservableCollection<Contact>(_contactService.ViewAllContacts());
    }


    [RelayCommand]
    private void GoToAddContactView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    [RelayCommand]
    private void GoBack()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
    }
}