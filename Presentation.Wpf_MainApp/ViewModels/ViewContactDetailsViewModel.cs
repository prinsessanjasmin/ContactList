using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.DTOs;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Security.RightsManagement;


namespace Presentation.Wpf_MainApp.ViewModels;

public partial class ViewContactDetailsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactServiceCRUD _contactService;

    public ViewContactDetailsViewModel(IServiceProvider serviceProvider, IContactServiceCRUD contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
    }

    [ObservableProperty]
    private string _title = "Contact Details";

    [ObservableProperty]
    private Contact _contact = null!;

    public void LoadContact(string contactId)
    {
        Contact = _contactService.FindContactById(contactId);
        if (Contact.FirstName == null)
        {
            throw new Exception("Contact not found.");
        }
    }

    [RelayCommand]
    private void EditContact(Contact contact)
    {
        ContactDto contactDto = ContactFactory.CreateContactDto(contact);
        var editOrRemoveContactViewModel = _serviceProvider.GetRequiredService<EditOrRemoveContactViewModel>();
        editOrRemoveContactViewModel.ContactToEdit = contactDto;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = editOrRemoveContactViewModel;
    }

    [RelayCommand]
    private void DeleteContact(Contact contact)
    {
        bool result = _contactService.DeleteContact(contact); 

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
