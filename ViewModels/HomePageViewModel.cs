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
        _mainWindow.AccountPage.Find();
    }

    [RelayCommand]
    public void GoToMovementPage()
    {
        _mainWindow.CurrentPage = _mainWindow.MovementPage;
    }

    [RelayCommand]
    public void GoToMovementReportPage()
    {
        _mainWindow.CurrentPage = _mainWindow.MovementReportPage;
        _mainWindow.MovementReportPage.Find(null);
    }
}
