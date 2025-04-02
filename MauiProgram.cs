using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.Json;

using System.Reflection;

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
                });

            builder.AddAppSettings();

            // Reads settings configurations
            string? omdbApiKey = builder.Configuration.GetValue<string>("OmdbApiKey");
            string? omdbDataEndpoint = builder.Configuration.GetValue<string>("OmdbDataEndpoint");
            string? omdbPosterEndpoint = builder.Configuration.GetValue<string>("OmdbPosterEndpoint");

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        /// <summary>
        /// Reads settings from settings file.
        /// </summary>
        /// <param name="builder"></param>
        private static void AddAppSettings(this MauiAppBuilder builder)
        {
            using Stream? stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("MovieRepoApp.appsettings.json");

            if (stream != null)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
                builder.Configuration.AddConfiguration(config);
            }
        }
    }
}
