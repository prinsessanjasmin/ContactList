using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Wpf_MainApp.ViewModels;

public partial class AddContactViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableObject _currentViewModel = null!;

    [ObservableProperty]
    private string _title = "Add New Contact";
}
