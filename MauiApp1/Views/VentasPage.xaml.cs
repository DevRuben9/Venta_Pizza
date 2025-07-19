using MauiApp1.Models;

namespace MauiApp1.Views;

public partial class VentasPage : ContentPage
{
	public VentasPage()
	{
		InitializeComponent();
	}
	private async void OnButtonClicked(object sender, EventArgs e)
	{
		await DisplayAlert("Alert", "Button clicked!", "OK");
    }
    
}