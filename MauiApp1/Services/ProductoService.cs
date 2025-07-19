using SQLite;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    class ProductoService
    {
        readonly SQLiteAsyncConnection _database;

        public ProductoService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Producto>().Wait();
        }

        public Task<List<Producto>> GetProductosAsync() =>
            _database.Table<Producto>().ToListAsync();

        public Task<int> SaveProductoAsync(Producto producto) =>
            _database.InsertAsync(producto);

        public Task<int> DeleteProductoAsync(Producto producto) =>
            _database.DeleteAsync(producto);
    }
}
