using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRepoApp.Services
{
    public class DialogueService : IDialogueService
    {
        public async Task ShowErrorAsync(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Error", message, "OK");
        }

        public async Task ShowMessageAsync(string message)
        {
            await Application.Current.MainPage.DisplayAlert("", message, "OK");
        }
    }
}
