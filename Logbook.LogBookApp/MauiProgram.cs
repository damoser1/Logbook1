using CommunityToolkit.Maui;
using Logbook.Lib;
using Logbook.LogBookApp.Pages;
using Logbook.LogBookCore.ViewModel;
using LogBook.LogBookCore.ViewModels;
using Microsoft.Extensions.Logging;

namespace Logbook.LogBookApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                // MauiCommunityToolkit verwenden
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<ReportPage>();
            builder.Services.AddSingleton<ReportViewModel>();

            string path = FileSystem.Current.AppDataDirectory;
            string filename = "data.xml";
            string fullpath = System.IO.Path.Combine(path, filename);
            System.Diagnostics.Debug.WriteLine(fullpath);

            builder.Services.AddSingleton<IRepository>(new XmlRepository(fullpath));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
