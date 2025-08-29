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
        private readonly MainWindowViewModel _mainWindow;
        public ObservableCollection<MovementRow> Movements { get; } = new();
        public int MovementCount { get; set; } = 0;
        public decimal CreditTotal { get; set; } = 0;
        public decimal DebtTotal { get; set; } = 0;
        public decimal Balance { get; set; } = 0;

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
        public async Task Find(string? accountCode)
        {
            Movements.Clear();

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
            MovementCount = Movements.Count;
        }
        
    }
}