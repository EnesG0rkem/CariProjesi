using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CariProje.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase currentPage;

    public HomePageViewModel HomePage { get; }
    public AccountPageViewModel AccountPage { get; }

    public MainWindowViewModel()
    {
        HomePage = new HomePageViewModel(this);
        AccountPage = new AccountPageViewModel(this);
        CurrentPage = HomePage;
    }
}
