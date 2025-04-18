namespace MovieRepoApp.UIComponents;

public partial class MovieDisplay : ContentView
{
	public static readonly BindableProperty PosterSourceProperty =
		BindableProperty.Create(nameof(PosterSource), typeof(string), typeof(MovieDisplay));

    public string PosterSource
	{
		get => (string)GetValue(PosterSourceProperty);
		set => SetValue(PosterSourceProperty, value);
	}

	public static readonly BindableProperty MovieTitleProperty =
		BindableProperty.Create(nameof(MovieTitle), typeof(string), typeof(MovieDisplay));

	public string MovieTitle
	{
		get => (string)GetValue(MovieTitleProperty);
		set => SetValue(MovieTitleProperty, value);
	}

	public static readonly BindableProperty MovieReleaseYearProperty =
		BindableProperty.Create(nameof(MovieReleaseYear), typeof(string), typeof(MovieDisplay));

	public string MovieReleaseYear
	{
		get => (string)GetValue(MovieReleaseYearProperty);
		set => SetValue(MovieReleaseYearProperty, value);
	}

	public static readonly BindableProperty MovieDescriptionProperty =
		BindableProperty.Create(nameof(MovieDescription), typeof(string), typeof(MovieDisplay));

	public string MovieDescription
	{
		get => (string)GetValue(MovieDescriptionProperty);
		set => SetValue(MovieDescriptionProperty, value);
	}

	public MovieDisplay()
	{
		InitializeComponent();
	}
}