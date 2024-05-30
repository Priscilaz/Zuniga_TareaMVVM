namespace PriscilaZunigaApuntesMaui;

public partial class PZAboutPage : ContentPage
{
	public PZAboutPage()
	{
		InitializeComponent();
	}

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://aka.ms/maui");
    }
}