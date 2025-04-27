using MovieRepoApp.ViewModels;

namespace MovieRepoApp.Views;

public partial class WishlistLibraryPage : ContentPage
{
    private WishlistLibraryViewModel _viewModel;

    public WishlistLibraryPage(WishlistLibraryViewModel viewModel)
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
