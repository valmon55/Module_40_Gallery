using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XMR.Gallery.Pages;

namespace XMR.Gallery
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void pin_Completed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PictureListPage());

            // скрываем введенный ПИН
            pin.Text = string.Empty;
        }
    }
}
