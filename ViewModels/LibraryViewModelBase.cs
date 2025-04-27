using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieRepoApp.Models;
using MovieRepoApp.Services;
using MovieRepoApp.Services.Repo;

namespace MovieRepoApp.ViewModels
{
    public abstract class LibraryViewModelBase : BaseViewModel
    {
        protected readonly IDialogueService _dialogueService;
        protected readonly IRepository<MovieEntity> _movieRepo;
        public ICommand DeleteCommand { get; private set; }

        public ICommand ToggleWishlistCommand { get; private set; }
        public ObservableCollection<MovieEntity> MovieEntities { get; set; } = [];

        public abstract Task InitializeAsync();
        public LibraryViewModelBase(IRepository<MovieEntity> movieRepo, IDialogueService dialogueService)
        {
            _movieRepo = movieRepo;
            _dialogueService = dialogueService;
            DeleteCommand = new AsyncCommand(ExecuteDelete, s => true);
            ToggleWishlistCommand = new AsyncCommand(ExecuteToggleWishlist, s => true);
        }

        protected async Task ExecuteDelete(object? parameter)
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
        protected async Task ExecuteToggleWishlist(object? parameter)
        {
            if (parameter is MovieEntity entity)
            {
                entity.OnWishlist = !entity.OnWishlist;
                await _movieRepo.UpdateAsync(entity);
                MovieEntities.Remove(entity);
                await _dialogueService.ShowMessageAsync("Movie successfully moved.");
            }
        }
    }
}