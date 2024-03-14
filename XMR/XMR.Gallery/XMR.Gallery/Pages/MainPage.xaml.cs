using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
            string pinCheck = string.Empty;
            string enteredPin = (((Entry)sender).Text ?? string.Empty);

            if (enteredPin.Length != 4)
            {
                await DisplayAlert("Ошибка", "ПИН должен быть из 4 знаков!", "Отмена");
                return;
            }

            if (!Preferences.ContainsKey("PIN"))
            {
                Preferences.Set("PIN", enteredPin);
            }
            else 
            {
                pinCheck = Preferences.Get("PIN", "");

                if (pinCheck != enteredPin)
                {
                    await DisplayAlert("Ошибка", "Неверный ПИН!", "Отмена");
                    return;
                }
            }
            //await Permissions.RequestAsync<Permissions.StorageRead>();
            //await Permissions.RequestAsync<Permissions.StorageWrite>();
            await Navigation.PushAsync(new PictureListPage("Галерея"));

            // скрываем введенный ПИН
            ((Entry)sender).Text = string.Empty;
        }

        private void clearPin(object sender, EventArgs e)
        {
            Preferences.Clear();
            pin.Text = string.Empty;
        }
    }
}
