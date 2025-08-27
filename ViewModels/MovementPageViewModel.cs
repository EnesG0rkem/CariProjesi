using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CariProjesi.Models;

namespace CariProje.ViewModels;

public partial class MovementPageViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindow;
    public MovementPageViewModel(MainWindowViewModel mainWindow)
    {
        _mainWindow = mainWindow;
    }

    [RelayCommand]
    public void GoToHomePage()
    {
        _mainWindow.CurrentPage = _mainWindow.HomePage;
    }
}