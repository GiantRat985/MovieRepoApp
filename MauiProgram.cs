using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.Json;

using System.Reflection;
using MovieRepoApp.ViewModels;
using MovieRepoApp.Views;
using MovieRepoApp.Services.Repo;
using MovieRepoApp.Services;
using MovieRepoApp.Models;

namespace MovieRepoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterServices()
                .RegisterViews()
                .RegisterViewModels()
                .RegisterSettings();

            builder.Configuration.AddJsonFile("appsettings.json", false, true);
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            InitializeDatabase(app);
            return app;
        }

        private static void InitializeDatabase(MauiApp app)
        {
            /*
            var conn = app.Services.GetRequiredService<ISQLiteConnectionFactory>().CreateAsyncConnection();
            conn.CreateTableAsync<MovieEntity>().Wait();
            conn.CreateTableAsync<MovieUserData>().Wait();
            */
            using var conn = app.Services.GetRequiredService<ISQLiteConnectionFactory>().CreateConnection();
            conn.CreateTable<MovieEntity>();
            conn.CreateTable<MovieUserData>();

        }

        /// <summary>
        /// Reads settings from settings file.
        /// </summary>
        /// <param name="builder"></param>
        private static MauiAppBuilder RegisterSettings(this MauiAppBuilder builder)
        {
            using Stream? stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("MovieRepoApp.appsettings.json");

                IConfigurationRoot config = new ConfigurationBuilder()
                    .AddJsonStream(stream!)
                    .Build();
                builder.Configuration.AddConfiguration(config);

            // Reads settings configurations
            builder.Services.AddSingleton<IConfiguration>(config);

            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<AddWatchedMovieViewModel>();
            builder.Services.AddScoped<WatchedLibraryViewModel>();

            return builder;
        }       
        
        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<AddWatchedMoviePage>();
            builder.Services.AddScoped<WatchedLibraryPage>();

            return builder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<IDialogueService, DialogueService>();

            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MovieRepo.db3");
            builder.Services.AddSingleton<ISQLiteConnectionFactory, SQLiteConnectionFactory>(s => new SQLiteConnectionFactory(dbPath));
            builder.Services.AddSingleton<IRepository<MovieEntity>, MovieRepository<MovieEntity>>();
            builder.Services.AddSingleton<IRepository<MovieUserData>, MovieRepository<MovieUserData>>();

            return builder;
        }
    }
}
