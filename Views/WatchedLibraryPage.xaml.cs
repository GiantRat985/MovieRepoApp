using MovieRepoApp.ViewModels;

namespace MovieRepoApp.Views;

public partial class WatchedLibraryPage : ContentPage
{
    private readonly WatchedLibraryViewModel _viewModel;
	public WatchedLibraryPage(WatchedLibraryViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		await _viewModel.InitializeAsync();
	}
}