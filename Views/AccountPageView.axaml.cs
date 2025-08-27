using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CariProje.ViewModels;
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
        Find(null, null);
    }


    private async void OnClickSave(object? sender, RoutedEventArgs e)
    {
        var account = await _accountService.GetByIdAsync(AccountCodeTextBox.Text);
        if (account != null)
        {
            account.AccountCode = AccountCodeTextBox.Text;
            account.AccountName = AccountNameTextBox.Text;
            account.AccountSurname = AccountSurnameTextBox.Text;
            account.AccountAddress = AccountAddressTextBox.Text;
            account.AccountDistrict = AccountDistrictTextBox.Text;
            account.AccountCity = AccountCityTextBox.Text;
            account.AccountCountry = AccountNationTextBox.Text;
            account.AccountPhone = AccountPhoneNumberTextBox.Text;
            account.AccountEmail = AccountEmailTextBox.Text;
            await _accountService.UpdateAsync(account);
            Find(null, null);
            return;
        }
        else
        {

            var newAccount = new Account
            {
                AccountCode = AccountCodeTextBox.Text,
                AccountName = AccountNameTextBox.Text,
                AccountSurname = AccountSurnameTextBox.Text,
                AccountAddress = AccountAddressTextBox.Text,
                AccountDistrict = AccountDistrictTextBox.Text,
                AccountCity = AccountCityTextBox.Text,
                AccountCountry = AccountNationTextBox.Text,
                AccountPhone = AccountPhoneNumberTextBox.Text,
                AccountEmail = AccountEmailTextBox.Text
            };

            await _accountService.AddAsync(newAccount);
            Find(null, null);

        }

    }

    private void Clear(object? sender, RoutedEventArgs e)
    {
        AccountCodeTextBox.Clear();
        AccountNameTextBox.Clear();
        AccountSurnameTextBox.Clear();
        AccountAddressTextBox.Clear();
        AccountDistrictTextBox.Clear();
        AccountCityTextBox.Clear();
        AccountNationTextBox.Clear();
        AccountPhoneNumberTextBox.Clear();
        AccountEmailTextBox.Clear();
        Find(null, null);
    }

    private async void Delete(object? sender, RoutedEventArgs e)
    {
        await _accountService.DeleteAsync(AccountCodeTextBox.Text);
        Find(null, null);
    }

    private async void Find(object? sender, RoutedEventArgs e)
    {
        AccountsList.Items.Clear();
        var accounts = (await _accountService.GetAllAsync()).Where(x =>
            x.AccountCode.Contains(AccountCodeTextBox.Text ?? "") &&
            x.AccountName.Contains(AccountNameTextBox.Text ?? "") &&
            x.AccountSurname.Contains(AccountSurnameTextBox.Text ?? "") &&
            x.AccountAddress.Contains(AccountAddressTextBox.Text ?? "") &&
            x.AccountDistrict.Contains(AccountDistrictTextBox.Text ?? "") &&
            x.AccountCity.Contains(AccountCityTextBox.Text ?? "") &&
            x.AccountCountry.Contains(AccountNationTextBox.Text ?? "") &&
            x.AccountPhone.Contains(AccountPhoneNumberTextBox.Text ?? "") &&
            x.AccountEmail.Contains(AccountEmailTextBox.Text ?? "")
        );
        foreach (Account account in accounts)
        {
            AccountsList.Items.Add(account);
        }
    }

    private void Fill(object? sender, RoutedEventArgs e)
    {
        var selectedItem = (Account)AccountsList.SelectedItem;
        if (selectedItem == null) return;
        AccountCodeTextBox.Text = selectedItem.AccountCode;
        AccountNameTextBox.Text = selectedItem.AccountName;
        AccountSurnameTextBox.Text = selectedItem.AccountSurname;
        AccountAddressTextBox.Text = selectedItem.AccountAddress;
        AccountDistrictTextBox.Text = selectedItem.AccountDistrict;
        AccountCityTextBox.Text = selectedItem.AccountCity;
        AccountNationTextBox.Text = selectedItem.AccountCountry;
        AccountPhoneNumberTextBox.Text = selectedItem.AccountPhone;
        AccountEmailTextBox.Text = selectedItem.AccountEmail;
    }

}