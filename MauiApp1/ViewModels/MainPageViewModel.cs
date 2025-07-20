using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly ProductoService _productoService;
        
        [ObservableProperty]
        private ObservableCollection<Producto> productos = new();

        [ObservableProperty]
        private bool isLoading;

        public MainPageViewModel()
        {
            // Inicializar el servicio de productos
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "productos.db");
            _productoService = new ProductoService(dbPath);
            
            // Cargar productos al inicializar
            _ = LoadProductosAsync();
        }

        [RelayCommand]
        private async Task LoadProductosAsync()
        {
            try
            {
                IsLoading = true;
                var productosFromDb = await _productoService.GetProductosAsync();
                
                Productos.Clear();
                foreach (var producto in productosFromDb)
                {
                    Productos.Add(producto);
                }

                // Si no hay productos, agregar algunos datos de ejemplo
                if (Productos.Count == 0)
                {
                    await SeedDatabaseAsync();
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al cargar productos: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task VerDetalles(Producto producto)
        {
            if (producto != null)
            {
                var estadoTexto = producto.Estado ? "Disponible" : "No disponible";
                await Shell.Current.DisplayAlert(
                    "Detalles del Producto", 
                    $"Producto: {producto.Nombre}\nCantidad en stock: {producto.Cantidad}\nEstado: {estadoTexto}", 
                    "OK");
            }
        }

        private async Task SeedDatabaseAsync()
        {
            try
            {
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
                    await _productoService.SaveProductoAsync(producto);
                }

                // Recargar la lista después de insertar los datos
                await LoadProductosAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al agregar productos de ejemplo: {ex.Message}", "OK");
            }
        }
    }
}