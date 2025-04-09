using MovieRepoApp.ViewModels;

namespace MovieRepoApp.Views;

public partial class AddWatchedMoviePage : ContentPage
{
	public AddWatchedMoviePage()
	{
		InitializeComponent();
        HandlerChanged += OnHandlerChanged;
    }

    private void OnHandlerChanged(object sender, EventArgs e)
    {
        BindingContext = Handler.MauiContext.Services.GetService<AddWatchedMovieViewModel>();
    }
}