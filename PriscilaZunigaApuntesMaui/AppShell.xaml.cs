namespace PriscilaZunigaApuntesMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.PZNotePage), typeof(Views.PZNotePage));
        }
    }
}
