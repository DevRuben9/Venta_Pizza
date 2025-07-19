using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        public ObservableCollection<Pizza> Pizzas { get; set; }

        public ICommand OrderPizzaCommand { get; }
        public ICommand NavigateCommand { get; }

        public MainPageViewModel()
        {
            Pizzas = new ObservableCollection<Pizza>
            {
                new Pizza
                {
                    Name = "Pizza Pepperoni",
                    Image = "pizza1.jpg", // Asegúrate de tener esta imagen en Resources/Images
                    Description = "La clásica pizza de pepperoni con mucho queso y salsa de tomate.",
                    amount = "PZ001",
                    Price = 12.99
                },
                new Pizza
                {
                    Name = "Pizza Hawaiana",
                    Image = "pizza2.jpg", // Asegúrate de tener esta imagen en Resources/Images
                    Description = "Deliciosa combinación de jamón, piña y queso mozzarella.",
                    amount = "PZ002",
                    Price = 13.50
                },
                new Pizza
                {
                    Name = "Pizza Vegetariana",
                    Image = "pizza3.jpg", // Asegúrate de tener esta imagen en Resources/Images
                    Description = "Frescas verduras de temporada, champiñones, pimientos y cebolla.",
                    amount = "PZ003",
                    Price = 14.25
                },
                new Pizza
                {
                    Name = "Pizza Cuatro Quesos",
                    Image = "pizza1.jpg", // Asegúrate de tener esta imagen en Resources/Images
                    Description = "Una explosión de sabor con mozzarella, parmesano, gorgonzola y provolone.",
                    amount = "PZ004",
                    Price = 15.00
                },
                 new Pizza
                {
                    Name = "Pizza Barbacoa",
                    Image = "pizza2.jpg", // Asegúrate de tener esta imagen en Resources/Images
                    Description = "Pollo a la barbacoa, cebolla morada y cilantro fresco.",
                    amount = "PZ005",
                    Price = 14.75
                }
            };

            // Define el comando para el botón de pedido
            OrderPizzaCommand = new RelayCommand<Pizza>(OnOrderPizza);
            NavigateCommand = new AsyncRelayCommand<string>(OnNavigate);
        }

        private async void OnOrderPizza(Pizza pizza)
        {
            if (pizza != null)
            {
                await Application.Current.MainPage.DisplayAlert("Pedido", $"Has pedido la pizza: {pizza.Name} (Código: {pizza.amount})", "OK");
                // Aquí podrías agregar lógica para añadir al carrito, navegar a una página de detalles, etc.
            }
        }

        private async Task OnNavigate(string route)
        {
            if (!string.IsNullOrWhiteSpace(route))
            {
                // La doble barra '//' indica navegación a una ruta de nivel superior en Shell
                await Shell.Current.GoToAsync($"//{route}");
            }
        }

    }
}
