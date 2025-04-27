using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRepoApp.Models;
using MovieRepoApp.Services;
using MovieRepoApp.Services.Repo;

namespace MovieRepoApp.ViewModels
{
    public class WishlistLibraryViewModel : LibraryViewModelBase
    {
        public WishlistLibraryViewModel(IRepository<MovieEntity> movieRepo, IDialogueService dialogueService) : base(movieRepo, dialogueService)
        {
        }

        public override async Task InitializeAsync()
        {
            var movies = await _movieRepo.QueryAsync("SELECT * FROM Movies WHERE OnWishlist = true");

            MovieEntities.Clear();
            foreach (var item in movies)
            {
                MovieEntities.Add(item);
            }
        }
    }
}
