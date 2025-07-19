using MauiApp1.Services;
namespace MauiApp1
{
    public partial class App : Application
    {
        static VentaDatabase database;
        public static IServiceProvider Services { get; private set; }
        public static VentaDatabase Database
        {
            get
            {
                if (database == null)
                {
                    var dbPath = Path.Combine(FileSystem.AppDataDirectory, "ventas.db3");
                    database = new VentaDatabase(dbPath);
                }
                return database;
            }
        }
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            Services = serviceProvider;
            MainPage = new AppShell();
        }
    }
}
