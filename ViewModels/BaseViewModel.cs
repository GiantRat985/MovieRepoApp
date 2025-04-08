using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieRepoApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event member for the <see cref="INotifyPropertyChanged"/> interface
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// Event invocation for the <see cref="INotifyPropertyChanged"/> interface
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
