using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieRepoApp.Models;
using MovieRepoApp.Services;
using MovieRepoApp.Services.Repo;

namespace MovieRepoApp.ViewModels
{
    public class WatchedLibraryViewModel
    {
        private readonly IRepository<MovieEntity> _movieRepo;
        private readonly IDialogueService _dialogueService;

        public ObservableCollection<MovieEntity> MovieEntities { get; set; } = [];
        public ICommand DeleteCommand { get; private set; }

        public WatchedLibraryViewModel(IRepository<MovieEntity> movieRepo, IDialogueService dialogueService)
        {
            _movieRepo = movieRepo;
            DeleteCommand = new AsyncCommand(ExecuteDelete, CanExecuteDelete);
            _dialogueService = dialogueService;
        }

        public async Task InitializeAsync()
        {
            var movies = await _movieRepo.GetAllAsync();

            MovieEntities.Clear();
            foreach (var item in movies)
            {
                MovieEntities.Add(item);
            }
        }

        private async Task ExecuteDelete(object? parameter)
        {
            try
            {
                if (parameter is MovieEntity entity)
                    {
                        await _movieRepo.DeleteAsync(entity);
                        await _dialogueService.ShowMessageAsync("Movie deleted.");
                        MovieEntities.Remove(entity);
                    }
                    else if (parameter == null)
                        throw new ArgumentNullException(nameof(parameter), "Item is null.");
                    else
                        throw new ArgumentException("Item is invalid.");
            }
            catch (Exception ex)
            {
                await _dialogueService.ShowErrorAsync(ex.Message);
            }
        }
        private bool CanExecuteDelete(object? parameter)
        {
            return true;
        }
    }
}
