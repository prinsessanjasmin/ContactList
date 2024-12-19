using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Wpf_MainApp.Views;

namespace Presentation.Wpf_MainApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [ObservableProperty]
    private ObservableObject _currentViewModel = null!;

    [ObservableProperty]
    private string _title = "Main Menu";


    [RelayCommand]
    private void GoToAddContact()
    {
        CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    [RelayCommand]
    private void GoToViewAllContacts()
    {
        CurrentViewModel = _serviceProvider.GetRequiredService<ViewAllContactsViewModel>();
    }

    [RelayCommand]
    private void GoToEditOrDeleteContact()
    {
        CurrentViewModel = _serviceProvider.GetRequiredService<EditOrRemoveContactViewModel>();
    }
}
