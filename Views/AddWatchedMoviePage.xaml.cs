using MovieRepoApp.ViewModels;

namespace MovieRepoApp.Views;

public partial class AddWatchedMoviePage : ContentPage
{
	public AddWatchedMoviePage(AddWatchedMovieViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}