using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using MauiApp1.Services;
using System.Threading.Tasks;

namespace MauiApp1
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
                    // Otros fonts si los tienes (ej. Font Awesome)
                });
            // --- ¡Registra tu DatabaseService aquí! ---
            builder.Services.AddSingleton<DatabaseService>();

            // --- Opcional: Inicializar la base de datos al inicio de la aplicación ---
            // Esto asegura que la DB y las tablas se creen tan pronto como la app inicie
            var app = builder.Build();
            var databaseService = app.Services.GetService<DatabaseService>();
            if (databaseService != null)
            {
                // Inicia la DB en un hilo de fondo para no bloquear la UI
                Task.Run(async () => await databaseService.Init()).Wait();
            }

            return app;
        }
    }
}
