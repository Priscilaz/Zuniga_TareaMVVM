namespace PriscilaZunigaApuntesMaui.Views;

public partial class PZAllNotesPage : ContentPage
{
	
    public PZAllNotesPage()
    {
        InitializeComponent();

        BindingContext = new Models.PZAllNotes();
    }

    protected override void OnAppearing()
    {
        ((Models.PZAllNotes)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PZNotePage));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.PZNote)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(PZNotePage)}?{nameof(PZNotePage.ItemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}