using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CariProje.ViewModels
{
    public partial class DialogViewModel : ViewModelBase
    {

        public DialogViewModel()
        {
        }

        [ObservableProperty]
        private bool _confirmed;

        protected TaskCompletionSource closeTask = new TaskCompletionSource();

        public async Task WaitAsync() => await closeTask.Task;

        public void Show()
        {
            if (closeTask.Task.IsCompleted)
                closeTask = new TaskCompletionSource();
        }

        public void Close()
        {
            closeTask.TrySetResult();
        }
    }

}