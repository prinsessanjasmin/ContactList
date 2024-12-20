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

    public ViewAllContactsViewModel(IServiceProvider serviceProvider, IContactService contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        ContactList = new ObservableCollection<Contact>(_contactService.ViewAllContacts());
    }

    [ObservableProperty]
    private string _title = "Your Contacts";

    [ObservableProperty]
    private ObservableCollection<Contact> _contactList = new();



    [RelayCommand]
    private void GoToAddContactView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    [RelayCommand]
    private void GoToViewContactDetailsView(Contact contact) 
    {
        var viewContactDetailsViewModel = _serviceProvider.GetRequiredService<ViewContactDetailsViewModel>();
        viewContactDetailsViewModel.Contact = contact;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = viewContactDetailsViewModel;
    }

    [RelayCommand]
    private void GoBack()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
    }
}