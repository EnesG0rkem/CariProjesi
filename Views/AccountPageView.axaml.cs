using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CariProjesi.Data;
using CariProjesi.Models;
using CariProjesi.Services;

namespace CariProje.Views;

public partial class AccountPageView : UserControl
{
    private readonly AccountService _accountService;
    public AccountPageView()
    {
        var dbContext = new ApplicationDbContext();
        var accountRepository = new GenericRepository<Account>(dbContext);
        _accountService = new AccountService(accountRepository);
        InitializeComponent();
    }

    private async void OnClickSave(object? sender, RoutedEventArgs e)
    {
        var account = new Account
        {
            AccountCode = AccountCodeTextBox.Text,
            AccountName = AccountNameTextBox.Text,
            AccountSurname= AccountSurnameTextBox.Text,
            AccountAddress = AccountAddressTextBox.Text,
            AccountDistrict = AccountDistrictTextBox.Text,
            AccountCity = AccountCityextBox.Text,
            AccountCountry = AccountNationTextBox.Text,
            AccountPhone = AccountPhoneNumberTextBox.Text,
            AccountEmail = AccountEmailTextBox.Text
        };

        await _accountService.AddAsync(account);
    
    }
}