using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.DTOs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

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

    /// <summary>
    /// Sends user to EditOrRemoveContactView.
    /// </summary>
    /// <param name="contact"></param>
    [RelayCommand]
    private void EditContact(Contact contact)
    {
        ContactDto contactDto = ContactFactory.CreateContactDto(contact);
        var editOrRemoveContactViewModel = _serviceProvider.GetRequiredService<EditOrRemoveContactViewModel>();
        editOrRemoveContactViewModel.ContactToEdit = contactDto;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = editOrRemoveContactViewModel;
    }

    /// <summary>
    /// When the user presses delete from the WiewContactDetailsView the chosen Contact is sent to this method. A message box appears 
    /// and prompts the user to confirm that they want to delete the contact. If they press "Cancel"/"Avbryt", the user will be sent back to the list of contact. 
    /// If they choose the "OK" option, the contact is then sent to the DeleteContact method in the ContactService, which returns <c>true</c> if the contact is successfully deleted. If so, the user is sent back to the list of contacts. 
    /// If not, the deleteResult variable return <c>false</c>, and the user is sent back to the list of contacts. 
    /// </summary>
    /// <param name="contact"></param>
    [RelayCommand]
    private void DeleteContact(Contact contact)
    {
        var result = MessageBox.Show($"Are you sure you want to delete {contact.FirstName}'s contact information?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        if (result == MessageBoxResult.OK)
        {
            bool deleteResult = _contactService.DeleteContact(contact);

            if (deleteResult)
            {
                Cancel();
            }
        }
        else
        {
            Cancel();
        }
    }

    /// <summary>
    /// Sends user back to the list of contacts.
    /// </summary>
    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ViewAllContactsViewModel>();
    }
}
