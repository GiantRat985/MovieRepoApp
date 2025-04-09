using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using MovieRepoApp.Models;
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
        private readonly string _apiKey;
        private readonly string _omdbUri;
        private string? _title;
        private MovieMetadata _metadata;
        private string _poster;
        public string? Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                    ((AsyncCommand)SearchCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public MovieMetadata Metadata
        {
            get => _metadata;
            set
            {
                if (_metadata != value)
                {
                    _metadata = value;
                    OnPropertyChanged();
                    ((AsyncCommand)SearchCommand).RaiseCanExecuteChanged();
                }
            }
        }
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
        public AddWatchedMovieViewModel(IConfiguration config, HttpClient client)
        {
            _config = config;
            _httpClient = client;
            _apiKey = _config["OmdbApiKey"];
            _omdbUri = _config["OmdbDataEndpoint"];

            SearchCommand = new AsyncCommand(ExecuteSearch, CanExecuteSearch);
        }
        /// <summary>
        /// Determines if the search command can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true if <see cref="Title"/> is not null.</returns>
        public bool CanExecuteSearch(object? parameter)
        {
            return (!string.IsNullOrWhiteSpace(Title));
        }
        /// <summary>
        /// Queries OMDB API with input information and sets the information to an object.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task ExecuteSearch(object? parameter)
        {
            var response = await QueryClient($"{_omdbUri}?apikey={_apiKey}&t={Title}");
            Metadata = ParseJson(response);
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
