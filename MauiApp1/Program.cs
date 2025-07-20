using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== DEMOSTRACIÓN DE RECUPERACIÓN DE DATOS ===");
            Console.WriteLine("Este programa demuestra cómo recuperar datos de la base de datos SQLite\n");

            // Configurar la ruta de la base de datos
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "productos.db");
            var productoService = new ProductoService(dbPath);

            Console.WriteLine($"Base de datos ubicada en: {dbPath}\n");

            try
            {
                // Verificar si hay productos en la base de datos
                var productos = await productoService.GetProductosAsync();

                if (productos.Count == 0)
                {
                    Console.WriteLine("No hay productos en la base de datos. Agregando datos de ejemplo...\n");
                    
                    // Agregar datos de ejemplo
                    var productosEjemplo = new List<Producto>
                    {
                        new Producto { Nombre = "Pizza Pepperoni", Cantidad = 25, Estado = true },
                        new Producto { Nombre = "Pizza Hawaiana", Cantidad = 18, Estado = true },
                        new Producto { Nombre = "Pizza Vegetariana", Cantidad = 22, Estado = true },
                        new Producto { Nombre = "Pizza Cuatro Quesos", Cantidad = 15, Estado = true },
                        new Producto { Nombre = "Pizza Barbacoa", Cantidad = 20, Estado = false }
                    };

                    foreach (var producto in productosEjemplo)
                    {
                        await productoService.SaveProductoAsync(producto);
                        Console.WriteLine($"✓ Agregado: {producto.Nombre}");
                    }

                    Console.WriteLine("\nDatos de ejemplo agregados exitosamente.\n");
                    
                    // Recuperar los datos nuevamente
                    productos = await productoService.GetProductosAsync();
                }

                // Mostrar los productos como aparecerían en las cards de la aplicación MAUI
                Console.WriteLine("=== PRODUCTOS RECUPERADOS DE LA BASE DE DATOS ===");
                Console.WriteLine("Así es como aparecerían en las cards de MainPage.xaml:\n");

                foreach (var producto in productos)
                {
                    Console.WriteLine("╔═══════════════════════════════════════╗");
                    Console.WriteLine("║              🍕 PRODUCTO               ║");
                    Console.WriteLine("╠═══════════════════════════════════════╣");
                    Console.WriteLine($"║ Nombre: {producto.Nombre.PadRight(26)} ║");
                    Console.WriteLine($"║ Cantidad en stock: {producto.Cantidad.ToString().PadRight(15)} ║");
                    Console.WriteLine($"║ ID: {producto.Id.ToString().PadRight(31)} ║");
                    Console.WriteLine($"║ Estado: {(producto.Estado ? "Disponible" : "No disponible").PadRight(26)} ║");
                    Console.WriteLine("║                                       ║");
                    Console.WriteLine($"║     [{(producto.Estado ? "✓ Ver Detalles" : "✗ No disponible").PadRight(20)}] ║");
                    Console.WriteLine("╚═══════════════════════════════════════╝");
                    Console.WriteLine();
                }

                Console.WriteLine($"Total de productos encontrados: {productos.Count}");
                Console.WriteLine("\n=== EXPLICACIÓN TÉCNICA ===");
                Console.WriteLine("1. MainPageViewModel usa ProductoService para acceder a la base de datos");
                Console.WriteLine("2. ProductoService utiliza SQLite para operaciones CRUD");
                Console.WriteLine("3. El modelo Producto define la estructura de datos");
                Console.WriteLine("4. MainPage.xaml muestra los datos en CollectionView con DataTemplate");
                Console.WriteLine("5. Los convertidores transforman el estado booleano en texto y colores");
                Console.WriteLine("\n=== CÓDIGO XAML CORRESPONDIENTE ===");
                Console.WriteLine("El MainPage.xaml actualizado incluye:");
                Console.WriteLine("- CollectionView con ItemsSource=\"{Binding Productos}\"");
                Console.WriteLine("- DataTemplate con modelo x:DataType=\"model:Producto\"");
                Console.WriteLine("- Binding a propiedades: {Binding Nombre}, {Binding Cantidad}, {Binding Estado}");
                Console.WriteLine("- Convertidores para mostrar estado como texto y colores");
                Console.WriteLine("- MainPageViewModel carga datos desde ProductoService al inicializar");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}