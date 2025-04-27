using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieRepoApp.Models;

namespace MovieRepoApp.UIComponents;

public partial class MovieCollectionEntityIcon : ContentView
{
	public static readonly BindableProperty PosterSourceProperty =
		BindableProperty.Create(nameof(PosterSource), typeof(string), typeof(MovieCollectionEntityIcon));
	public string PosterSource
	{
		get => (string)GetValue(PosterSourceProperty);
		set => SetValue(PosterSourceProperty, value);
	}

    public MovieCollectionEntityIcon()
	{
		InitializeComponent();
	}
}