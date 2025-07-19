using MauiApp1.ViewModels;
namespace MauiApp1.Views;

public partial class InventarioPage : ContentPage
{
	public InventarioPage()
	{
		InitializeComponent();
        BindingContext = new InventarioViewModel();
    }

}