using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CariProjesi.Data;
using CariProjesi.Models;
using CariProjesi.Services;

namespace CariProje.Views
{
    public partial class MovementPageView : UserControl
    {
        private readonly MovementService _movementService;
        public MovementPageView()
        {
            var dbContext = new ApplicationDbContext();
            var movementRepository = new GenericRepository<Movement>(dbContext);
            var accountRepsoitory = new GenericRepository<Account>(dbContext);
            _movementService = new MovementService(movementRepository, accountRepsoitory);

            InitializeComponent();

            MovementDatePicker.SelectedDate = DateTime.Now.Date;
            MovementTimePicker.SelectedTime = DateTime.Now.TimeOfDay;

            AccountNameTextBox_OnTextChanged(null, null);
        }

        private async void OnClickSave(object? sender, RoutedEventArgs e)
        {
            bool check;

            if ((MovementTypeComboBox.SelectedItem as ComboBoxItem).Content.ToString().Equals("Borç"))
                check = false;
            else check = true;

            DateOnly date = DateOnly.FromDateTime(MovementDatePicker.SelectedDate!.Value.DateTime);
            TimeOnly time = TimeOnly.FromTimeSpan(MovementTimePicker.SelectedTime!.Value);

            DateTime dateTime = date.ToDateTime(time);

            var newMovement = new Movement
            {
                AccountCode = AccountCodeTextBox.Text,
                MovementId = Guid.NewGuid(),
                MovementDate = dateTime,
                MovementDescription = MovementDescriptionTextBox.Text,
                MovementType = check,
                MovementChange = decimal.Parse(MovementAmountTextBox.Text)

            };
            
            await _movementService.AddAsync(newMovement);
        }

        private async void Clear(object? sender, RoutedEventArgs e)
        {
            AccountCodeTextBox.Clear();
            AccountNameTextBox.Clear();
            MovementDatePicker.Clear();
            MovementTimePicker.Clear();
            MovementDescriptionTextBox.Clear();
            MovementAmountTextBox.Clear();
            MovementTypeComboBox.Clear();
        }

        private async void AccountNameTextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
        {
            AccountNameTextBox.Text = (await _movementService.FindAccountNameAsync(AccountCodeTextBox.Text)) ?? "Cari bulunamadı";
        }
        
    }
}

