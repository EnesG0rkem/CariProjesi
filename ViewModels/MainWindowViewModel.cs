using CommunityToolkit.Mvvm.ComponentModel;

namespace CariProje.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase currentPage;

    public HomePageViewModel HomePage { get; }
    public AccountPageViewModel AccountPage { get; }
    public MovementPageViewModel MovementPage { get; }
    public MovementReportPageViewModel MovementReportPage { get; }

    public MainWindowViewModel()
    {
        HomePage = new HomePageViewModel(this);
        AccountPage = new AccountPageViewModel(this);
        MovementPage = new MovementPageViewModel(this);
        MovementReportPage = new MovementReportPageViewModel(this);

        CurrentPage = HomePage;
    }
}
