using Business.DTOs;
using Business.Interfaces;
using Business.Models;
using Business.Factories;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation.Wpf_MainApp.ViewModels;

public partial class EditOrRemoveContactViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactServiceCRUD _contactService;

    [ObservableProperty]
    private ContactDto _contactToEdit;

    public EditOrRemoveContactViewModel(IServiceProvider serviceProvider, IContactServiceCRUD contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        ContactList = new ObservableCollection<Contact>(_contactService.ViewAllContacts());
        CurrentViewModel = this;
        ContactToEdit = ContactFactory.CreateContactDto(Contact);
    }

    [ObservableProperty]
    private ObservableObject _currentViewModel;

    [ObservableProperty]
    private ObservableCollection<Contact> _contactList = new();

    [ObservableProperty]
    private string _title = "Edit or Remove Contact";

    [ObservableProperty]
    private Contact _contact = new();



    [RelayCommand]
    private void SaveContact(ContactDto ContactToEdit)
    {

        ContactToEdit.ValidateModel();

        if (ContactToEdit.HasErrors)
        {
            return;
        }
        else
        {
            Contact updatedContact = ContactFactory.CreateContact(ContactToEdit);
            bool result = _contactService.UpdateContact(updatedContact);

            if (result)
            {
                var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
                mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ViewAllContactsViewModel>();
            }
        }
    }

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