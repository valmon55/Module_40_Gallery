using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMR.Gallery.Model;

namespace XMR.Gallery.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureListPage : ContentPage
    {
        public static string PageName { get; set; }
        public List<Picture> Pictures { get; set; } = new List<Picture>();
        public PictureListPage(string pageName)
        {
            PageName = pageName;
            
            //File.AppendAllText("Test.txt", "Test-Test-Test-Test-Test-Test");
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            Pictures.Add(new Picture(driveInfo[0].Name, driveInfo[0].Name + " " + driveInfo[1].Name));
            Pictures.Add(new Picture("VOLUME1/DCIM/Camera/20230506_100622.jpg", "Картинка 1"));
            Pictures.Add(new Picture("VOLUME1\\DCIM\\Camera\\20230508_130949.jpg", "Картинка 2"));

            Pictures.AddRange(GetAllCameraPictures());

            InitializeComponent();

            BindingContext = this;
        }
        private List<Picture> GetAllCameraPictures()
        {
            List<Picture> list = new List<Picture>();

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                // Проверяем доступность диска перед использованием
                if (drive.IsReady)
                {
                    // Получаем список файлов в директории DCIM/Camera для каждого доступного диска
                    string cameraDirectory = Path.Combine(drive.Name, "DCIM", "Camera");
                    if (Directory.Exists(cameraDirectory))
                    {
                        // Получаем список файлов картинок в директории камеры
                        string[] imageFiles = Directory.GetFiles(cameraDirectory, "*.jpg");
                        foreach (var imageFile in imageFiles)
                        {
                            // Используем относительный путь для создания объекта Picture
                            //string relativePath = Path.GetRelativePath(drive.Name, imageFile);
                            list.Add(new Picture(Path.GetFullPath(imageFile), Path.GetFileName(imageFile)));
                        }
                    }
                }
            }
            return list;
        }
        private List<Picture> GetAllPictures()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            
            List<Picture> list = new List<Picture>();

            foreach(var drive in drives)
            {
                list.AddRange(GetAllDrivePicture(drive.RootDirectory,"jpg"));
            }
            return list;
        }
        private List<Picture> GetAllDrivePicture(DirectoryInfo directory, string filter)
        {
            List<Picture> list = new List<Picture>();
            try
            {
                foreach (var dir in directory.GetDirectories())
                {
                    list.AddRange(GetDirFiles(dir, filter));

                }
            }
            catch(Exception ex) { }
            return list;
        }
        private List<Picture> GetDirFiles(DirectoryInfo directory, string filter)
        {
            List<Picture> list = new List<Picture>();
            try
            {
                foreach (var file in directory.GetFiles())
                {
                    if (file.Name.Contains(filter))
                    {
                        list.Add(new Picture(directory.Name, file.Name));
                    }
                }
            }
            catch(Exception ex) { } 
            return list;
        }
        private async void OpenPicture(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PictureViewPage("Картинка 1"));
        }
        private async void DeletePicture(object sender, EventArgs e)
        {
            await DisplayAlert("Удаление", "Удаляем картинку", "Отмена");
        }
        private void GetPictures(string pictureGallery) 
        { 
            
        }
    }
}