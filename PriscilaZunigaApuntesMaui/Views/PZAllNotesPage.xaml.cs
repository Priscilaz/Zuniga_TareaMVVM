namespace PriscilaZunigaApuntesMaui.Views;

public partial class PZAllNotesPage : ContentPage
{
	
    public PZAllNotesPage()
    {
        InitializeComponent();

        //BindingContext = new Models.PZAllNotes();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        pznotesCollection.SelectedItem = null;
    }

    
}