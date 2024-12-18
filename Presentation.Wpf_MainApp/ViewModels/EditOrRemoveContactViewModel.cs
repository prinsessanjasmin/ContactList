using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf_MainApp.ViewModels;

public partial class EditOrRemoveContactViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private ObservableObject _currentViewModel;

    [ObservableProperty]
    private string _title = "Edit or Remove Contact";

    [RelayCommand]
    private void GoBack()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
    }

    public EditOrRemoveContactViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        CurrentViewModel = this; 
    }
}