using CommunityToolkit.Mvvm.Input;

namespace CariProje.ViewModels;

public partial class HomePageViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindow;

    public HomePageViewModel(MainWindowViewModel mainWindow)
    {
        _mainWindow = mainWindow;
    }

    [RelayCommand]
    public void GoToAccountPage()
    {
        _mainWindow.CurrentPage = _mainWindow.AccountPage;
    }

    
}
