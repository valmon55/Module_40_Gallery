using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XMR.Gallery.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureListPage : ContentPage
    {
        public PictureListPage()
        {
            InitializeComponent();
        }

        private async void OpenPicture(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PictureViewPage());
        }
        private void DeletePicture(object sender, EventArgs e)
        {
            DisplayAlert("Удаление", "Удаляем картинку", "Отмена");
        }
    }
}