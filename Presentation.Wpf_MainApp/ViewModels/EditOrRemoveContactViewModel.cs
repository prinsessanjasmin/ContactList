using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.Wpf_MainApp.ViewModels;

public partial class EditOrRemoveContactViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableObject _currentViewModel = null!;

    [ObservableProperty]
    private string _title = "Edit Or Remove Contact";
}