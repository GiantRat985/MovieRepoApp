using MovieRepoApp.ViewModels;

namespace MovieRepoApp.Views;

public partial class MovieSearchContentView : ContentView
{
	public MovieSearchContentView()
	{
		InitializeComponent();
		HandlerChanged += OnHandlerChanged;
	}

	private void OnHandlerChanged(object sender, EventArgs e)
	{
		BindingContext = Handler.MauiContext.Services.GetService<MovieSearchViewModel>();
	}
}