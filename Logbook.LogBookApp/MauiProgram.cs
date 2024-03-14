using CommunityToolkit.Maui;
using Logbook.Lib;
using Logbook.LogBookApp.Pages;
using Logbook.LogBookApp.Services;
using Logbook.LogBookCore.ViewModel;
using LogBook.LogBookCore.Services;
using LogBook.LogBookCore.ViewModels;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

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
                // SyncfusionCore Kit für den Chart
                .ConfigureSyncfusionCore()
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

            builder.Services.AddSingleton <IAlertService, AlertService> ();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
