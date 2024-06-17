using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PriscilaZunigaApuntesMaui.ViewModels;

internal class PZNoteViewModel : ObservableObject, IQueryAttributable
{
    private Models.PZNote _pznote;

    public string PZText
    {
        get => _pznote.Text;
        set
        {
            if (_pznote.Text != value)
            {
                _pznote.Text = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime PZDate => _pznote.Date;

    public string PZIdentifier => _pznote.Filename;

    public ICommand PZSaveCommand { get; private set; }
    public ICommand PZDeleteCommand { get; private set; }

    public PZNoteViewModel()
    {
        _pznote = new Models.PZNote();
        PZSaveCommand = new AsyncRelayCommand(PZSave);
        PZDeleteCommand = new AsyncRelayCommand(PZDelete);
    }

    public PZNoteViewModel(Models.PZNote pznote)
    {
        _pznote = pznote;
        PZSaveCommand = new AsyncRelayCommand(PZSave);
        PZDeleteCommand = new AsyncRelayCommand(PZDelete);
    }

    private async Task PZSave()
    {
        _pznote.Date = DateTime.Now;
        _pznote.PZSave();
        await Shell.Current.GoToAsync($"..?saved={_pznote.Filename}");
    }

    private async Task PZDelete()
    {
        _pznote.PZDelete();
        await Shell.Current.GoToAsync($"..?deleted={_pznote.Filename}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            _pznote = Models.PZNote.PZLoad(query["load"].ToString());
            PZRefreshProperties();
        }
    }
    public void PZReload()
    {
        _pznote = Models.PZNote.PZLoad(_pznote.Filename);
        PZRefreshProperties();
    }

    private void PZRefreshProperties()
    {
        OnPropertyChanged(nameof(PZText));
        OnPropertyChanged(nameof(PZDate));
    }
}
