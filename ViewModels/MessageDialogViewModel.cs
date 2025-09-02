using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CariProje.ViewModels
{
    public partial class MessageDialogViewModel : DialogViewModel
    {

        [ObservableProperty] private string _title;
        [ObservableProperty] private string _message ;
        [ObservableProperty] private string _closeText;

        [RelayCommand]
        public void CloseDialog()
        {
            Close();
        }
    }
}