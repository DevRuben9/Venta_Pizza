using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    public class InventarioViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Producto> Productos { get; set; } = new();
        public ICommand addProductoCommand { get; }
        public ICommand ModificarCommand { get;  }
        public string TipoProductoSeleccionado { get; set; }
        public string CantidadProducto { get; set; }

        private readonly ProductoService _productoService;

        public InventarioViewModel()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "productos.db3");
            _productoService = new ProductoService(dbPath);

            addProductoCommand = new Command(async () => await AgregarProducto());
            ModificarCommand = new Command<Producto>(async (producto) => await ModificarProducto(producto));
            LoadProductos();
        }

        private async void LoadProductos()
        {
            var productos = await _productoService.GetProductosAsync();
            Productos.Clear();
            foreach (var producto in productos)
                Productos.Add(producto);
        }

        private async Task AgregarProducto()
        {
            if (!string.IsNullOrEmpty(TipoProductoSeleccionado) &&
                int.TryParse(CantidadProducto, out int cantidad))
            {
                var producto = new Producto
                {
                    Nombre = TipoProductoSeleccionado,
                    Cantidad = cantidad,
                    Estado = true
                };
                await _productoService.SaveProductoAsync(producto);
                LoadProductos();
                CantidadProducto = string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task ModificarProducto(Producto producto)
        {
            // Aquí puedes abrir un popup, página nueva o directamente editar los datos
            producto.Nombre += " (Editado)";
            await _productoService.SaveProductoAsync(producto); // Save actualiza si tiene ID
            LoadProductos();
        }
    }
}
