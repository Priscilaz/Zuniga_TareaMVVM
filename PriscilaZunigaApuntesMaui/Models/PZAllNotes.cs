using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriscilaZunigaApuntesMaui.Models
{
    
    internal class PZAllNotes
    {
        public ObservableCollection<PZNote> Notes { get; set; } = new ObservableCollection<PZNote>();

        public PZAllNotes() =>
            LoadNotes();

        public void LoadNotes()
        {
            Notes.Clear();

            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            IEnumerable<PZNote> notes = Directory

                                        // Select the file names from the directory
                                        .EnumerateFiles(appDataPath, "*.PZnotes.txt")

                                        // Each file name is used to create a new Note
                                        .Select(filename => new PZNote()
                                        {
                                            Filename = filename,
                                            Text = File.ReadAllText(filename),
                                            Date = File.GetLastWriteTime(filename)
                                        })

                                        // With the final collection of notes, order them by date
                                        .OrderBy(note => note.Date);

            // Add each note into the ObservableCollection
            foreach (PZNote note in notes)
                Notes.Add(note);
        }
    }
}
