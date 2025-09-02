using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CariProje.ViewModels
{
    public partial class ConfirmDialogViewModel : DialogViewModel
    {
        public ConfirmDialogViewModel(){}

        [ObservableProperty] private string _title = "Onayla";
        [ObservableProperty] private string _message = "Emin misiniz?";
        [ObservableProperty] private string _confirmText = "Evet";
        [ObservableProperty] private string _cancelText = "Hayır";

        [RelayCommand]
        public async Task Confirm()
        {
            Confirmed = true;
            Close();
        }

        [RelayCommand]
        public async Task Cancel()
        {
            Confirmed = false;
            Close();
        }
    }
}