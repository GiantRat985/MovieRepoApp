using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRepoApp.ViewModels
{
    public interface IMovieSearch : INotifyPropertyChanged
    {
        public string? Title { get; set; }
    }
}
