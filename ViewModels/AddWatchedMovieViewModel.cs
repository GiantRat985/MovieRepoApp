using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using MovieRepoApp.Models;
using MovieRepoApp.Services;
using MovieRepoApp.Services.Repo;
using Newtonsoft.Json;

namespace MovieRepoApp.ViewModels
{
    /// <summary>
    /// Handles movie search functions.
    /// </summary>
    public class AddWatchedMovieViewModel : BaseViewModel
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly IRepository<MovieEntity> _movieRepository;
        private readonly string _apiKey;
        private readonly string _omdbUri;
        private readonly IDialogueService _dialogueService;

        private MovieMetadata _metadata;
        public MovieMetadata Metadata
        {
            get => _metadata;
            set
            {
                if (_metadata != value)
                {
                    _metadata = value;
                    OnPropertyChanged();
                    ((AsyncCommand)AddCommand).RaiseCanExecuteChanged();
                }
            }
        }
        private string? _search;
        public string? SearchText
        {
            get => _search;
            set
            {
                if (_search != value)
                {
                    _search = value;
                    OnPropertyChanged();
                    ((AsyncCommand)SearchCommand).RaiseCanExecuteChanged();
                }
            }
        }
        private string? _title;
        public string? Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _releaseYear;
        public string ReleaseYear
        {
            get => _releaseYear;
            set
            {
                if (_releaseYear != value)
                {
                    _releaseYear = value;
                    OnPropertyChanged();
                }
            }
        }
        private string? _description;
        public string? Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }
        private string? _poster;
        public string? Poster
        {
            get => _poster;
            set
            {
                if (_poster != value)
                {
                    _poster = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Command for the search button.
        /// </summary>
        public ICommand SearchCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="client"></param>
        public AddWatchedMovieViewModel(IConfiguration config, HttpClient client, IRepository<MovieEntity> repository, IDialogueService dialogueService)
        {
            _config = config;
            _httpClient = client;
            _movieRepository = repository;
            _dialogueService = dialogueService;
            _apiKey = _config["OmdbApiKey"];
            _omdbUri = _config["OmdbDataEndpoint"];

            SearchCommand = new AsyncCommand(ExecuteSearch, CanExecuteSearch);
            AddCommand = new AsyncCommand(ExecuteAdd, CanExecuteAdd);
        }
        /// <summary>
        /// Takes currently selected movie and adds it to database asynchronously.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task ExecuteAdd(object? parameter)
        {
            try
            {
                if (bool.Parse(Metadata.Response) != true)
                {
                    throw new ArgumentException("Data is corrupt.");
                }
                await _movieRepository.AddAsync(
                    new MovieEntity
                    {
                        ImdbID = Metadata.ImdbID,
                        Title = Metadata.Title,
                        Year = int.Parse(Metadata.Year),
                        Rated = Metadata.Rated,
                        Genre = Metadata.Genre,
                        Director = Metadata.Director,
                        Writer = Metadata.Writer,
                        Actors = Metadata.Actors,
                        Plot = Metadata.Plot,
                        Country = Metadata.Country,
                        Poster = Metadata.Poster,
                        ImdbRating = Metadata.ImdbRating,
                        Type = Metadata.Type
                    });
                await _dialogueService.ShowMessageAsync("Movie added.");
            }
            catch (ArgumentException ex)
            {
                await _dialogueService.ShowErrorAsync(ex.Message);
            }
        }
        public bool CanExecuteAdd(object? parameter)
        {
            if (Metadata == null)
            {
                return false;
            }
            else if (bool.Parse(Metadata.Response) != true)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Determines if the search command can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if <see cref="SearchText"/> is not null.</returns>
        public bool CanExecuteSearch(object? parameter)
        {
            return (!string.IsNullOrWhiteSpace(SearchText));
        }
        /// <summary>
        /// Queries OMDB API with input information and sets the information to an object.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task ExecuteSearch(object? parameter)
        {
            var response = await QueryClient($"{_omdbUri}?apikey={_apiKey}&t={SearchText}");
            Metadata = ParseJson(response);
            Title = Metadata.Title;
            ReleaseYear = Metadata.Year;
            Description = Metadata.Plot;
            Poster = Metadata.Poster;
        }
        /// <summary>
        /// Sends GET request to http client.
        /// </summary>
        /// <param name="getRequest">GET query string.</param>
        /// <returns>JSON string response.</returns>
        public async Task<string> QueryClient(string getRequest)
        {
            var response = await _httpClient.GetAsync(getRequest);

            try
            {
                var data = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                return data;
            }
            catch(HttpRequestException ex)
            {
                return $"Request failed: {ex.Message}";
            }
            catch(Exception ex)
            {
                return $"An error has occured: {ex.Message}";
            }
        }
        /// <summary>
        /// Parses JSON string into <see cref="MovieMetadata"/> object.
        /// </summary>
        /// <param name="json">JSON string.</param>
        /// <returns><see cref="MovieMetadata"/> object.</returns>
        private MovieMetadata ParseJson(string json)
        {
            var metadata = new MovieMetadata();
            metadata = JsonConvert.DeserializeObject<MovieMetadata>(json);
            return metadata;
        }
    }
}
