using Android.Graphics;
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
    public partial class PictureViewPage : ContentPage
    {
        public static string ImageName { get; set; }
        public static string ImageDateStr { get; set; }
        public Model.Picture picture { get; set; }
        public static string pictureFile { get; set; }
        public PictureViewPage(Model.Picture _picture)
        {
            ImageName = _picture.Name;
            picture = _picture;
            pictureFile = _picture.FullFileName;
            ImageDateStr = "Создана " + _picture.CreationDate.ToLongDateString() + " в " +_picture.CreationDate.ToLongTimeString();
            InitializeComponent();
        }
    }
}