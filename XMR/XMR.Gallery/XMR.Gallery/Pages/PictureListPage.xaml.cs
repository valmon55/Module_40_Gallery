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
        public static string PageName { get; set; }
        public PictureListPage(string pageName)
        {
            PageName = pageName;

            InitializeComponent();
        }
        private async void OpenPicture(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PictureViewPage("Картинка 1"));
        }
        private void DeletePicture(object sender, EventArgs e)
        {
            DisplayAlert("Удаление", "Удаляем картинку", "Отмена");
        }
    }
}