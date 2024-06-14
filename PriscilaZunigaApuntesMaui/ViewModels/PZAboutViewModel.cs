using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace PriscilaZunigaApuntesMaui.ViewModels
{
    internal class PZAboutViewModel
    {
        public string PZTitle => AppInfo.Name;
        public string PZVersion => AppInfo.VersionString;
        public string PZMoreInfoUrl => "https://aka.ms/maui";
        public string PZMessage => "This app is written in XAML and C# with .NET MAUI by Priscila Zuniga";
        public ICommand PZShowMoreInfoCommand { get; }

        public PZAboutViewModel()
        {
            PZShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
        }

        async Task ShowMoreInfo() =>
            await Launcher.Default.OpenAsync(PZMoreInfoUrl);


    }
}
