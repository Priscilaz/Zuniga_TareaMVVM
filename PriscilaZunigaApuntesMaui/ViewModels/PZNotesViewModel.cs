using CommunityToolkit.Mvvm.Input;
using PriscilaZunigaApuntesMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PriscilaZunigaApuntesMaui.ViewModels
{
    internal class PZNotesViewModel : IQueryAttributable
    {
        public ObservableCollection<ViewModels.PZNoteViewModel> PZAllNotes { get; }
        public ICommand? PZNewCommand { get; }
        public ICommand? PZSelectNoteCommand { get; }

        public PZNotesViewModel()
        {
            PZAllNotes = new ObservableCollection<ViewModels.PZNoteViewModel>(Models.PZNote.LoadAllPZ().Select(n => new PZNoteViewModel(n)));
            PZNewCommand = new AsyncRelayCommand(PZNewNoteAsync);
            PZSelectNoteCommand = new AsyncRelayCommand<ViewModels.PZNoteViewModel>(PZSelectNoteAsync);
        }

        private async Task PZNewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.PZNotePage));
        }

        private async Task PZSelectNoteAsync(ViewModels.PZNoteViewModel pznote)
        {
            if (pznote != null)
                await Shell.Current.GoToAsync($"{nameof(Views.PZNotePage)}?load={pznote.PZIdentifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                PZNoteViewModel matchedNote = PZAllNotes.Where((n) => n.PZIdentifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    PZAllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                PZNoteViewModel matchedNote = PZAllNotes.Where((n) => n.PZIdentifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                    matchedNote.PZReload();

                // If note isn't found, it's new; add it.
                else
                    PZAllNotes.Add(new PZNoteViewModel(PZNote.LoadPZ(noteId)));
            }
        }
    }
}
