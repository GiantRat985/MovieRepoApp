using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using MovieRepoApp.Models;
using Newtonsoft.Json;

namespace MovieRepoApp.ViewModels
{
    public class MovieSearchViewModel : BaseViewModel, IMovieSearch
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

        public ICommand SearchCommand { get; private set; }

        public MovieSearchViewModel(IConfiguration config, HttpClient client)
        {
            _config = config;
            _httpClient = client;
            _apiKey = _config["OmdbApiKey"];
            _omdbUri = _config["OmdbDataEndpoint"];

            SearchCommand = new AsyncCommand(ExecuteSearch, CanExecuteSearch);
        }

        public bool CanExecuteSearch(object? parameter)
        {
            return (!string.IsNullOrWhiteSpace(Title));
        }

        public async Task ExecuteSearch(object? parameter)
        {
            var response = await QueryClient($"{_omdbUri}?apikey={_apiKey}&t={Title}");
            Metadata = ParseJson(response);
            Poster = Metadata.Poster;
        }

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

        private MovieMetadata ParseJson(string json)
        {
            var metadata = new MovieMetadata();
            metadata = JsonConvert.DeserializeObject<MovieMetadata>(json);
            return metadata;
        }
    }
}
