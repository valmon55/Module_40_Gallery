using Android.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using XMR.Gallery.Model;
using System.Runtime.InteropServices.ComTypes;
using Java.Util;

namespace XMR.Gallery.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureListPage : ContentPage
    {
        public static string PageName { get; set; }
        public ObservableCollection<Picture> Pictures { get; set; } = new ObservableCollection<Picture>();
        private Picture currentPicture { get; set; }
        public PictureListPage(string pageName)
        {
            PageName = pageName;
            
            Pictures = GetAllCameraPictures();

            InitializeComponent();

            BindingContext = this;
        }
        private ObservableCollection<Picture> GetAllCameraPictures()
        {
            ObservableCollection<Picture> list = new ObservableCollection<Picture>();

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
                        string[] imageFiles = Directory.GetFiles(cameraDirectory, "*.jpg").OrderByDescending(x => x).Take(20).ToArray();
                        foreach (var imageFile in imageFiles)
                        {
                            // Используем относительный путь для создания объекта Picture - нет в версии standart 2.0
                            //string relativePath = Path.GetRelativePath(drive.Name, imageFile);
                            list.Add(new Picture(Path.GetFullPath(imageFile), Path.GetFileName(imageFile), File.GetCreationTime(imageFile)));                            
                        }
                    }
                }
            }
            return list;
        }
        private async void OpenPicture(object sender, EventArgs e)
        {
            if (currentPicture is null)
            {
                await DisplayAlert("Картинка", "Выберете картинку", "OK");
            }
            else
            {
                await Navigation.PushAsync(new PictureViewPage(currentPicture));
            }
        }
        private async void CreatePhoto(object sender, EventArgs e)
        {
            try 
            { 
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });
                // для примера сохраняем файл в локальном хранилище
                //var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                var newFile = Path.Combine(GetAvailableDir(), photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
            }
            catch(Exception ex) 
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
        private string GetAvailableDir()
        {
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
                        return cameraDirectory;
                    }
                }
            }
            return string.Empty;
        }
        private async void DeletePicture(object sender, EventArgs e)
        {
            var delStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (delStatus != PermissionStatus.Granted)
            {
                await DisplayAlert($"StorageWrite permission is {delStatus.ToString()}", "Attempt to get it!", "OK");
                try
                {
                    delStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            await DisplayAlert("StorageWrite permission", delStatus.ToString(), "OK");
            try
            {
                if (currentPicture is null)
                {
                    await DisplayAlert("Картинка", "Выберете картинку", "OK");
                }
                else
                {
                    if (File.Exists(currentPicture.FullFileName))
                    {
                        File.Delete(currentPicture.FullFileName);
                        await DisplayAlert("Удаление", $"Картинка {currentPicture.Name} удалена", "OK");
                        Pictures.Remove(currentPicture);
                    }
                    else
                    {
                        await DisplayAlert("Удаление", $"Картинка {currentPicture.Name} не найден", "OK");
                    }
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Ошибка", $"Возникла ошибка: {ex.Message}", "OK");
            }
            
        }
        private async void pictureList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                currentPicture = (Picture)e.SelectedItem;
            }
            else
            {
                await DisplayAlert("Картинка", "Выберете картинку", "OK");
            }
        }
    }
}