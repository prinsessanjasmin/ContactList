using Business.DTOs;
using Business.Interfaces;
using Business.Models;
using Business.Factories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

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


    /// <summary>
    /// When pressing the save button the ContactDto that the user has edited is validated via the ContactDto class. If any of the input fields does not contain
    /// valid values the contact will not be saved and the user remains in the form for adding a new contact. 
    /// If the values are valid the dto is sent to the UpdateContact method, which will return an updated Contact object. 
    /// The CreateNewContact method will return true if everything works, and then the if statement will send the user back to the list of contacts, where the 
    /// updated contact will now be visible. 
    /// </summary>
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
                GoToListOfContacts();
            }
        }
    }

    /// <summary>
    /// When the user presses delete from the EditOrRemoveContactView the ContactDto which the user is updating is sent to this method. A message box appears 
    /// and prompts the user to confirm that they want to delete the contact. If they press "Cancel"/"Avbryt", the user will be sent back to the list of contact. 
    /// If they choose the "OK" option, the ContactDto is sent to the ContactFactory which creates a Contact, which is sent back. The Contact is then sent to the 
    /// DeleteContact method in the ContactService, which returns <c>true</c> if the contact is successfully deleted. If so, the user is sent back to the list of contacts. 
    /// If not, the deleteResult variable return <c>false</c>, and the user is sent back to the list of contacts. 
    /// </summary>
    /// <param name="contactDto"></param>
    [RelayCommand]
    private void DeleteContact(ContactDto contactDto)
    {
        var result = MessageBox.Show($"Are you sure you want to delete {contactDto.FirstName}'s contact information?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        if (result == MessageBoxResult.OK)
        {
            Contact contactToDelete = ContactFactory.CreateContact(contactDto);
            bool deleteResult = _contactService.DeleteContact(contactToDelete);

            if (deleteResult)
            {
                GoToListOfContacts();
            }
        }
        else
        {
            GoToListOfContacts();
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        GoToListOfContacts();
    }

    /// <summary>
    /// Sends user back to the list of contacts.
    /// </summary>
    private void GoToListOfContacts()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ViewAllContactsViewModel>();
    }
}