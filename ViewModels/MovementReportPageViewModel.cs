using CommunityToolkit.Mvvm.Input;

namespace CariProje.ViewModels
{
    public partial class MovementReportPageViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _mainWindow;
        public MovementReportPageViewModel(MainWindowViewModel mainWindow)
        {
            _mainWindow = mainWindow;
        }

        [RelayCommand]
        public void GoToHomePage()
        {
            _mainWindow.CurrentPage = _mainWindow.MovementReportPage;
        }
    }
}