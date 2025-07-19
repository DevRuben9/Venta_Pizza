using SQLite;
using MauiApp1.Models;
using System.IO;

namespace MauiApp1.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            
        }

        // Método para inicializar la base de datos y crear las tablas
        public async Task Init()
        {
            if (_database != null) // Si ya está inicializada, no hagas nada
                return;

            // Define la ruta donde se guardará el archivo de la base de datos
            // FileSystem.AppDataDirectory es una ubicación segura y persistente en todas las plataformas
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "PizzaDB.db3");

            // Inicializa la conexión asíncrona a SQLite
            _database = new SQLiteAsyncConnection(databasePath);

            // Crea las tablas si no existen
            // Asegúrate de añadir todas las tablas que necesites aquí
            await _database.CreateTableAsync<Producto>();
            // await _database.CreateTableAsync<OtraTabla>(); // Si tienes más tablas

            // Opcional: Llamar a un método para insertar datos iniciales si la DB está vacía
            await SeedData();
        }

        // Método para insertar datos de ejemplo si la tabla está vacía
        private async Task SeedData()
        {
            // Solo inserta datos si la tabla de Productos está vacía
            if (await _database.Table<Producto>().CountAsync() == 0)
            {
                await _database.InsertAsync(new Producto { Nombre = "Pizza Grande", Cantidad = 100 });
                await _database.InsertAsync(new Producto { Nombre = "Pizza Especiales", Cantidad = 50 });
                await _database.InsertAsync(new Producto { Nombre = "Pizza Medianos", Cantidad = 25 });
                await _database.InsertAsync(new Producto { Nombre = "Pizza Pequeños", Cantidad = 25 });
                await _database.InsertAsync(new Producto { Nombre = "Pizza Mediano Especial", Cantidad = 5 });
            }
        }

        // --- Métodos CRUD para Producto ---

        // Obtener todos los productos
        public async Task<List<Producto>> GetProductosAsync()
        {
            await Init(); // Asegura que la BD esté inicializada
            return await _database.Table<Producto>().ToListAsync();
        }

        // Obtener un producto por ID
        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            await Init();
            return await _database.Table<Producto>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        // Guardar/Actualizar un producto
        public async Task<int> SaveProductoAsync(Producto producto)
        {
            await Init();
            if (producto.Id != 0) // Si el ID es diferente de 0, el producto ya existe (actualizar)
            {
                return await _database.UpdateAsync(producto);
            }
            else // Si el ID es 0, es un nuevo producto (insertar)
            {
                return await _database.InsertAsync(producto);
            }
        }

        // Eliminar un producto
        public async Task<int> DeleteProductoAsync(Producto producto)
        {
            await Init();
            return await _database.DeleteAsync(producto);
        }
    }
}
