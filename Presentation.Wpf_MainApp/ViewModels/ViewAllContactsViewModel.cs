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
    private readonly IContactServiceCRUD _contactService;

    public ViewAllContactsViewModel(IServiceProvider serviceProvider, IContactServiceCRUD contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        UpdateList();

        _contactService.ContactListUpdated += (sender, e) =>
        {
            UpdateList();
        };
    }

    public void UpdateList()
    {
        ContactList.Clear();
        foreach (var contact in _contactService.ViewAllContacts())
        {
            ContactList.Add(contact);
        }
    }


    [ObservableProperty]
    private string _title = "Your Contacts";

    [ObservableProperty]
    private ObservableCollection<Contact> _contactList = new();


    /// <summary>
    /// This command takes the user to the Add Contact Form. 
    /// </summary>
    [RelayCommand]
    private void GoToAddContactView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    /// <summary>
    /// This command first checks which contact is being sent as a parameter, then takes the user to the details view of that 
    /// particular Contact. 
    /// </summary>
    /// <param name="contact"></param>
    [RelayCommand]
    private void GoToViewContactDetailsView(Contact contact) 
    {
        var viewContactDetailsViewModel = _serviceProvider.GetRequiredService<ViewContactDetailsViewModel>();
        viewContactDetailsViewModel.Contact = contact;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = viewContactDetailsViewModel;
    }

    /// <summary>
    /// Sends user back to the start page.
    /// </summary>
    [RelayCommand]
    private void GoBack()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
    }
}