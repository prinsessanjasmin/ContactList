using Business.Interfaces;
using Business.Models;
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

    public EditOrRemoveContactViewModel(IServiceProvider serviceProvider, IContactServiceCRUD contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        ContactList = new ObservableCollection<Contact>(_contactService.ViewAllContacts());
        CurrentViewModel = this;
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
    private void SaveContact(Contact contact)
    {
        //Contact.ValidateModel();


        bool result = _contactService.UpdateContact(contact);

        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ViewAllContactsViewModel>();
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