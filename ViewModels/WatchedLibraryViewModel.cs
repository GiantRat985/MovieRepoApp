using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRepoApp.Models;
using MovieRepoApp.Services.Repo;

namespace MovieRepoApp.ViewModels
{
    public class WatchedLibraryViewModel
    {
        private readonly IRepository<MovieEntity> _movieRepo;

        public ObservableCollection<MovieEntity> MovieEntities { get; set; } = [];

        public WatchedLibraryViewModel(IRepository<MovieEntity> movieRepo)
        {
            _movieRepo = movieRepo;
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
    }
}
