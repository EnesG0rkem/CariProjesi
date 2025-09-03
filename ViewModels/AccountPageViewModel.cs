using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CariProjesi.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CariProjesi.Services;
using CariProjesi.Data;
using System;

namespace CariProje.ViewModels;

public partial class AccountPageViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindow;
    private readonly AccountService _accountService;

    private async Task<bool> ShowConfirmationDialog(string title, string message)
    {
        var dialog = new ConfirmDialogViewModel();
        dialog.Title = title;
        dialog.Message = message;
        dialog.ConfirmText = "Evet";
        dialog.CancelText = "Kapat";
        CurrentDialog = dialog;

        await dialog.WaitAsync();
        CurrentDialog = null;
        return dialog.Confirmed;
    }
    private async Task ShowMessageDialog(string title, string message)
    {
        var dialog = new MessageDialogViewModel();
        dialog.Title = title;
        dialog.Message = message;
        dialog.CloseText = "Tamam";
        CurrentDialog = dialog;
        await dialog.WaitAsync();
        CurrentDialog = null;

    }

    // Form fields
    [ObservableProperty] private string _accountCode = string.Empty;
    [ObservableProperty] private string _accountName = string.Empty;
    [ObservableProperty] private string _accountSurname = string.Empty;
    [ObservableProperty] private string _accountAddress = string.Empty;
    [ObservableProperty] private string _accountDistrict = string.Empty;
    [ObservableProperty] private string _accountCity = string.Empty;
    [ObservableProperty] private string _accountCountry = string.Empty;
    [ObservableProperty] private string _accountPhone = string.Empty;
    [ObservableProperty] private string _accountEmail = string.Empty;

    // List and selection
    public ObservableCollection<Account> Accounts { get; } = new();

    [ObservableProperty]
    private Account _selectedAccount;

    public AccountPageViewModel(MainWindowViewModel mainWindow)
    {
        _mainWindow = mainWindow;

        // Simple composition root here; could be moved to DI later
        var dbContext = new ApplicationDbContext();
        var accountRepository = new GenericRepository<Account>(dbContext);
        _accountService = new AccountService(accountRepository);

        // Initial load
        _ = Find();
        _selectedAccount = null!;
    }
    
    [RelayCommand]
    public void GoToHomePage()
    {
        _mainWindow.CurrentPage = _mainWindow.HomePage;
    }

    [RelayCommand]
    public void GoToMovementPage()
    {
        _mainWindow.CurrentPage = _mainWindow.MovementPage;
        _mainWindow.MovementPage.SetAccountCode(AccountCode);
    }

    [RelayCommand]
    public void GoToReportPage()
    {
        _mainWindow.CurrentPage = _mainWindow.MovementReportPage;
        _mainWindow.MovementReportPage.SetAccountCode(AccountCode);
    }
    
    partial void OnSelectedAccountChanged(Account value)
    {
        if (value == null)
            return;

        AccountCode = value.AccountCode;
        AccountName = value.AccountName;
        AccountSurname = value.AccountSurname;
        AccountAddress = value.AccountAddress;
        AccountDistrict = value.AccountDistrict;
        AccountCity = value.AccountCity;
        AccountCountry = value.AccountCountry;
        AccountPhone = value.AccountPhone;
        AccountEmail = value.AccountEmail;
    }

    [RelayCommand]
    public async Task Find()
    {
        Accounts.Clear();
        var all = await _accountService.GetAllAsync();
        var filtered = all.Where(x =>
            (x.AccountCode ?? string.Empty).Contains(AccountCode ?? string.Empty) &&
            (x.AccountName ?? string.Empty).Contains(AccountName ?? string.Empty) &&
            (x.AccountSurname ?? string.Empty).Contains(AccountSurname ?? string.Empty) &&
            (x.AccountAddress ?? string.Empty).Contains(AccountAddress ?? string.Empty) &&
            (x.AccountDistrict ?? string.Empty).Contains(AccountDistrict ?? string.Empty) &&
            (x.AccountCity ?? string.Empty).Contains(AccountCity ?? string.Empty) &&
            (x.AccountCountry ?? string.Empty).Contains(AccountCountry ?? string.Empty) &&
            (x.AccountPhone ?? string.Empty).Contains(AccountPhone ?? string.Empty) &&
            (x.AccountEmail ?? string.Empty).Contains(AccountEmail ?? string.Empty)
        );

        foreach (var account in filtered)
        {
            Accounts.Add(account);
        }
    }

    [RelayCommand]
    public async Task Save()
    {
        var code = (AccountCode ?? string.Empty).Trim();
        var name = (AccountName ?? string.Empty).Trim();
        var surname = (AccountSurname ?? string.Empty).Trim();
        var address = (AccountAddress ?? string.Empty).Trim();
        var district = (AccountDistrict ?? string.Empty).Trim();
        var city = (AccountCity ?? string.Empty).Trim();
        var country = (AccountCountry ?? string.Empty).Trim();
        var phone = (AccountPhone ?? string.Empty).Trim();
        var email = (AccountEmail ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(address) ||
            string.IsNullOrWhiteSpace(district) || string.IsNullOrWhiteSpace(city) ||
            string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(phone) ||
            string.IsNullOrWhiteSpace(email))
        {
            await ShowMessageDialog("Hata", "Lütfen tüm alanları doldurunuz.");
            return;
        }

        try
        {
            int.Parse(phone);
        }
        catch (Exception)
        {
            await ShowMessageDialog("Hata", "Telefon numarası geçerli bir sayı olmalıdır.");
            return;
        }

        var existing = await _accountService.GetByIdAsync(code);
        if (existing != null)
        {
            existing.AccountCode = code;
            existing.AccountName = name;
            existing.AccountSurname = surname;
            existing.AccountAddress = address;
            existing.AccountDistrict = district;
            existing.AccountCity = city;
            existing.AccountCountry = country;
            existing.AccountPhone = phone;
            existing.AccountEmail = email;

            var result = await ShowConfirmationDialog("Emin misiniz?", "Cari verilerini güncellemek istiyor musunuz?");
            if (!result) return;
            
            await _accountService.UpdateAsync(existing);
            await ShowMessageDialog("Başarılı", "Cari başarıyla güncellendi.");
        }
        else
        {
            var newAccount = new Account
            {
                AccountCode = code,
                AccountName = name,
                AccountSurname = surname,
                AccountAddress = address,
                AccountDistrict = district,
                AccountCity = city,
                AccountCountry = country,
                AccountPhone = phone,
                AccountEmail = email,
            };
            await _accountService.AddAsync(newAccount);
            await ShowMessageDialog("Başarılı", "Yeni cari başarıyla eklendi.");
        }
    }

    [RelayCommand]
    public async Task Delete()
    {
        if (string.IsNullOrWhiteSpace(AccountCode))
        {
            await ShowMessageDialog("Hata", "Silinecek cari seçilmedi.");
            return;
        }

        var account = await _accountService.GetByIdAsync(AccountCode);
        if (account == null)
        {
            await ShowMessageDialog("Hata", "Cari bulunamadı.");
            return;
        }

        var result = await ShowConfirmationDialog("Emin misiniz?", "Cariyi silmek istediğinize emin misiniz?");
        if (!result) return;
        
        
        await _accountService.DeleteAsync(AccountCode);
        await ShowMessageDialog("Başarılı!", "Cari başırılı bir şekilde silindi.");
        await Find();
    }

    [RelayCommand]
    public void Clear()
    {
        AccountCode = string.Empty;
        AccountName = string.Empty;
        AccountSurname = string.Empty;
        AccountAddress = string.Empty;
        AccountDistrict = string.Empty;
        AccountCity = string.Empty;
        AccountCountry = string.Empty;
        AccountPhone = string.Empty;
        AccountEmail = string.Empty;

        _ = Find();
    }
}