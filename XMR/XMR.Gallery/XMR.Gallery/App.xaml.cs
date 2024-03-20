using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XMR.Gallery
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            GetRWPermissions();
            MainPage = new NavigationPage(new MainPage());
        }
        private async Task GetRWPermissions()
        {
            await Permissions.RequestAsync<Permissions.StorageRead>();
            await Permissions.RequestAsync<Permissions.StorageWrite>();
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
