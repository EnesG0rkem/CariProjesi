using CommunityToolkit.Mvvm.ComponentModel;

namespace CariProje.ViewModels;

public partial class ViewModelBase : ObservableObject
{
       // Dialog overlay
    [ObservableProperty]
    private DialogViewModel? _currentDialog;
}
