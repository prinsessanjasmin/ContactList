using Business.DTOs;
using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf_MainApp.ViewModels;

/// <summary>
/// This is the view model the user gets when pressing the Add New Contact Button (or the button with a plus sign).
/// </summary>
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

    /// <summary>
    /// When pressing the save button the ContactDto that the user has added is validated via the ContactDto class. If any of the input fields does not contain
    /// valid values the contact will not be saved and the user remains in the form for adding a new contact. 
    /// If the values are valid a new contact will be created using the ContactDto. 
    /// The CreateNewContact method will return true if everything works, and then the if statement will send the user back to the list of contacts, where the 
    /// new contact will now be visible. 
    /// </summary>
    [RelayCommand]
    private void Save()
    {
        NewContact.ValidateModel();

        if (NewContact.HasErrors) 
        {
            return; 
        }
        else
        {
            var result = _contactService.CreateNewContact(NewContact);

            if (result)
            {
                var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
                mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ViewAllContactsViewModel>();
            }
        }
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
