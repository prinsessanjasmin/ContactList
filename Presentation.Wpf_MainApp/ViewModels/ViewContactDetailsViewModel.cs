﻿using Business.Interfaces;
using Business.Models;
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
        //ContactList = new ObservableCollection<Contact>(_contactService.ViewAllContacts());
    }

    [ObservableProperty]
    private string _title = "Contact Details";

    [ObservableProperty]
    private Contact _contact = null!;

    //[ObservableProperty]
    //private ObservableCollection<Contact> _contactList = [];

    [RelayCommand]
    private void EditContact(Contact contact)
    {
        var editOrRemoveContactViewModel = _serviceProvider.GetRequiredService<EditOrRemoveContactViewModel>();
        editOrRemoveContactViewModel.Contact = contact;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = editOrRemoveContactViewModel;
    }

    [RelayCommand]
    private void DeleteContact(Contact contact)
    {
        bool result = _contactService.DeleteContact(contact); 

        if (result)
        {
            //ContactList = new ObservableCollection<Contact>(_contactService.ViewAllContacts());
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
