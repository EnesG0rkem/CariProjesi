using CommunityToolkit.Mvvm.Input;

namespace CariProje.ViewModels;

public partial class AccountPageViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindow;
    public AccountPageViewModel(MainWindowViewModel mainWindow)
    {
        _mainWindow = mainWindow;
    }
    
    [RelayCommand]
    public void GoToHomePage()
    {
        _mainWindow.CurrentPage = _mainWindow.HomePage;
    }
}