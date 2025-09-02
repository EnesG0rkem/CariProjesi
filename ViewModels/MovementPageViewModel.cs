using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CariProjesi.Data;
using CariProjesi.Models;
using CariProjesi.Services;

namespace CariProje.ViewModels;

public partial class MovementPageViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindow;
    private readonly MovementService _movementService;
    private readonly GenericRepository<Account> _accountRepository;

    #region Properties

    private string _accountCode = string.Empty;
    public string AccountCode
    {
        get => _accountCode;
        set 
        { 
            if (SetProperty(ref _accountCode, value))
            {
                _ = LoadAccountNameAsync();
            }
        }
    }

    private string _accountName = string.Empty;
    public string AccountName
    {
        get => _accountName;
        set => SetProperty(ref _accountName, value);
    }

    // DatePicker expects DateTimeOffset? in Avalonia
    private DateTimeOffset? _movementDate = DateTimeOffset.Now.Date;
    public DateTimeOffset? MovementDate
    {
        get => _movementDate;
        set => SetProperty(ref _movementDate, value);
    }

    // TimePicker expects TimeSpan? in Avalonia
    private TimeSpan? _movementTime = DateTime.Now.TimeOfDay;
    public TimeSpan? MovementTime
    {
        get => _movementTime;
        set => SetProperty(ref _movementTime, value);
    }

    private string _movementDescription = string.Empty;
    public string MovementDescription
    {
        get => _movementDescription;
        set => SetProperty(ref _movementDescription, value);
    }

    private decimal _movementAmount;
    public decimal MovementAmount
    {
        get => _movementAmount;
        set => SetProperty(ref _movementAmount, value);
    }

    private int _selectedMovementTypeIndex = 0;
    public int SelectedMovementTypeIndex
    {
        get => _selectedMovementTypeIndex;
        set => SetProperty(ref _selectedMovementTypeIndex, value);
    }

    public ObservableCollection<string> MovementTypes { get; } = new()
    {
        "Borç",
        "Alacak"
    };

    private bool _isSaving;
    public bool IsSaving
    {
        get => _isSaving;
        set => SetProperty(ref _isSaving, value);
    }

    #endregion

    public MovementPageViewModel(MainWindowViewModel mainWindow, MovementService? movementService = null)
    {
        _mainWindow = mainWindow;
        
        // Initialize services - use dependency injection if provided, otherwise create instances
        if (movementService != null)
        {
            _movementService = movementService;
            // Note: In a real DI scenario, we would also inject the account repository
            var dbContext = new ApplicationDbContext();
            _accountRepository = new GenericRepository<Account>(dbContext);
        }
        else
        {
            // Fallback to manual initialization for backward compatibility
            var dbContext = new ApplicationDbContext();
            var movementRepository = new GenericRepository<Movement>(dbContext);
            var accountRepository = new GenericRepository<Account>(dbContext);
            _accountRepository = accountRepository;
            _movementService = new MovementService(movementRepository, accountRepository);
        }
    }

    #region Commands

    [RelayCommand]
    public void GoToHomePage()
    {
        _mainWindow.CurrentPage = _mainWindow.HomePage;
    }

    [RelayCommand]
    public void GoToAccountPage()
    {
        _mainWindow.CurrentPage = _mainWindow.AccountPage;
    }

    [RelayCommand]
    public void GoToReportPage()
    {
        _mainWindow.CurrentPage = _mainWindow.MovementReportPage;
        _mainWindow.MovementReportPage.SetAccountCode(AccountCode);
    }

    [RelayCommand]
    public void SetAccountCode(string accountCode)
    {
        AccountCode = accountCode;
    }

    [RelayCommand]
    private async Task SaveMovementAsync()
    {
     
            if (string.IsNullOrWhiteSpace(AccountCode))
            {
                // TODO: Show validation message
                return;
            }

            if (MovementAmount <= 0)
            {
                // TODO: Show validation message
                return;
            }

            var account = await _accountRepository.GetByIdAsync(AccountCode);
            if (account == null)
            {
                // TODO: Show error message
                return;
            }

            bool isCredit = SelectedMovementTypeIndex == 1; // 0: Borç, 1: Alacak
            
            // Combine date and time properly
            DateTime movementDateTime;
            if (MovementDate.HasValue && MovementTime.HasValue)
            {
                movementDateTime = MovementDate.Value.Date.Add(MovementTime.Value);
            }
            else
            {
                movementDateTime = DateTime.Now;
            }

            var newMovement = new Movement
            {
                AccountCode = AccountCode,
                AccountName = account.AccountName + " " + account.AccountSurname,
                MovementId = Guid.NewGuid(),
                MovementDate = movementDateTime,
                MovementDescription = MovementDescription,
                MovementType = isCredit,
                MovementChange = MovementAmount
            };

            await _movementService.AddAsync(newMovement);
    }

    

    [RelayCommand]
    private void ClearForm()
    {
        AccountCode = string.Empty;
        AccountName = string.Empty;
        MovementDate = DateTimeOffset.Now.Date;
        MovementTime = DateTime.Now.TimeOfDay;
        MovementDescription = string.Empty;
        MovementAmount = 0;
        SelectedMovementTypeIndex = 0;
    }

    #endregion

    #region Private Methods

    private async Task LoadAccountNameAsync()
    {
        var accountName = await _movementService.FindAccountNameAsync(AccountCode);
            AccountName = accountName ?? "Cari bulunamadı";
        
    }

    #endregion
}