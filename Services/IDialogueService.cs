using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRepoApp.Services
{
    public interface IDialogueService
    {
        Task ShowMessageAsync(string message);
        Task ShowErrorAsync(string message);
    }
}
