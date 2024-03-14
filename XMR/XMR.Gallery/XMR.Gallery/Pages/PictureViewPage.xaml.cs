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
        public PictureViewPage(string imageName)
        {
            ImageName = imageName;

            InitializeComponent();
        }
    }
}