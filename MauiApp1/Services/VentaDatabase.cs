using MauiApp1.Models;
using SQLite;

namespace MauiApp1.Services
{
    public class VentaDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public VentaDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Venta>().Wait();
        }

        public Task<int> SaveVentaAsync(Venta venta)
        {
            return _database.InsertAsync(venta);
        }

        public Task<List<Venta>> GetVentasAsync()
        {
            return _database.Table<Venta>().ToListAsync();
        }
    }
}
