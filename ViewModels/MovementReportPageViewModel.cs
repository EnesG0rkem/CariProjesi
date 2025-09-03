using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CariProje.Models;
using CariProjesi.Data;
using CariProjesi.Models;
using CariProjesi.Services;
using CommunityToolkit.Mvvm.Input;

namespace CariProje.ViewModels
{
    public partial class MovementReportPageViewModel : ViewModelBase
    {
        #region properties
        private readonly MainWindowViewModel _mainWindow;
        public ObservableCollection<MovementRow> Movements { get; } = new();

        private int _movementCount = 0;
        public int MovementCount
        {
            get => _movementCount;
            set => SetProperty(ref _movementCount, value);
        }

        private decimal _creditTotal = 0;
        public decimal CreditTotal
        {
            get => _creditTotal;
            set => SetProperty(ref _creditTotal, value);
        }

        private decimal _debtTotal = 0;
        public decimal DebtTotal
        {
            get => _debtTotal;
            set => SetProperty(ref _debtTotal, value);
        }

        private decimal _balance = 0;
        public decimal Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }

        private readonly MovementService _movementService;
        private string? _accountCode;
        public string? AccountCode
        {
            get => _accountCode;
            set
            {
                SetProperty(ref _accountCode, value);
                FindCommand.Execute(value);
            }
        }
        #endregion
        public MovementReportPageViewModel(MainWindowViewModel mainWindow)
        {
            _mainWindow = mainWindow;
            _movementService = new MovementService(new GenericRepository<Movement>(new ApplicationDbContext()),
                new GenericRepository<Account>(new ApplicationDbContext()));
            _ = Find(null);
        }

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
        public async Task Find(string? accountCode)
        {
            Movements.Clear();
            CreditTotal = 0;
            DebtTotal = 0;
            Balance = 0;
            MovementCount = 0;
            IEnumerable<Movement> movements;
            if (string.IsNullOrEmpty(accountCode))
            {
                movements = await _movementService.GetAllAsync();
            }
            else
            {
                movements = (await _movementService.GetAllAsync())
                    .Where(x => x.AccountCode == accountCode);
            }

            decimal runningTotal = 0;
            foreach (var movement in movements)
            {
                if (movement.MovementType)
                {
                    runningTotal += movement.MovementChange;
                    CreditTotal += movement.MovementChange;
                }
                else
                {
                    runningTotal -= movement.MovementChange;
                    DebtTotal += movement.MovementChange;
                }
                Movements.Add(new MovementRow(movement, runningTotal));
            }
            Balance = CreditTotal - DebtTotal;
            MovementCount = 0;
            MovementCount = Movements.Count;
        }

        [RelayCommand]
        public void Clear()
        {
            AccountCode = "";
        }

        [RelayCommand]
        public void SetAccountCode(string accountCode)
        {
            AccountCode = accountCode;
        }
    }
}