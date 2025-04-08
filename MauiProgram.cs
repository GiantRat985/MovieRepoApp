using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.Json;

using System.Reflection;
using MovieRepoApp.ViewModels;
using MovieRepoApp.Views;

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

            return builder.Build();
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
                    .AddJsonStream(stream)
                    .Build();
                builder.Configuration.AddConfiguration(config);

            // Reads settings configurations
            builder.Services.AddSingleton<IConfiguration>(config);

            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<MovieSearchViewModel>();

            return builder;
        }       
        
        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<MovieSearchContentView>();
            builder.Services.AddTransient<AddWatchedMoviePage>();

            return builder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<HttpClient>();

            return builder;
        }
    }
}
