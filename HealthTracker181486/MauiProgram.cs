using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using HealthTracker181486.Data;

namespace HealthTracker181486
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(_ => new HealthDatabase(Path.Combine(FileSystem.AppDataDirectory, "HealthTrack.db3")));

            builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddSingleton<DailySummaryRepository>();
            builder.Services.AddSingleton<ExerciseSessionRepository>();
            builder.Services.AddSingleton<HealthMeasurementRepository>();

            return builder.Build();
        }
    }
}
