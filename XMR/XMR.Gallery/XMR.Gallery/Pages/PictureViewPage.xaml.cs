﻿using Android.Graphics;
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
        public Model.Picture picture { get; set; }
        public static string pictureFile { get; set; }
        public PictureViewPage(Model.Picture _picture)
        {
            ImageName = "Картинка";
            picture = _picture;
            pictureFile = _picture.Path;
                //System.IO.Path.Combine(_picture.Path, _picture.Name);
            InitializeComponent();
        }
    }
}