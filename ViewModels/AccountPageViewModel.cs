using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CariProjesi.Models;

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