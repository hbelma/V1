using Plugin.Media;
using Plugin.Media.Abstractions;
using skylineapp.Helpers;
using skylineapp.Models;
using skylineapp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace skylineapp.ViewModels
{
    public class UploadPhotoViewModel
    {
        MediaFile mediaFile;
        ImageTable imageT;
        ImageManager imageManager = ImageManager.DefaultManager;
        INavigation navigation;

        private ImageSource _imageSource = "addphoto.png";

        public ImageSource ImageS
        {
            get { return _imageSource; }
            set { _imageSource = value; }
        }

        private ICommand pickPhotoCommand;
        public ICommand PickPhotoCommand { get { return pickPhotoCommand; } }

        private ICommand uploadPhotoCommand;
        public ICommand UploadPhotoCommand { get { return uploadPhotoCommand; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public ImageTable ImageT
        {
            get => imageT;
            set
            {
                imageT = value;
                OnPropertyChanged("ImageT");
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public UploadPhotoViewModel(INavigation nav, String cat) {
            imageT = new ImageTable();
            ImageT.Category = cat;
            this.Navigation = nav;
            pickPhotoCommand = new Command(async() => await PickPhoto());
            uploadPhotoCommand = new Command(async () => await SavePhoto());

        }

        private async Task SavePhoto()
        {
            if (ImageT.Desc == "" || mediaFile.Path == null)
                return;

            else
            {
                await UploadPhoto(mediaFile);
                ImageT.User = Settings.UserName;
                await imageManager.SaveTaskAsync(ImageT);
                await Navigation.PushAsync(new ImageWrapLayoutPage(ImageT.Category));
            }
        }

        private async Task PickPhoto()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }

            mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (mediaFile == null)
                return;

            this.ImageS = ImageSource.FromStream(() =>
            {
                var stream = mediaFile.GetStream();
                mediaFile.Dispose();
                return stream;
            });

        }

        public INavigation Navigation { get => navigation; set => navigation = value; }

        private async Task UploadPhoto(MediaFile mediaFile)
        {
           
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(mediaFile.GetStream()), "\"file\"", $"\"{mediaFile.Path}\"");
            var httpClient = new HttpClient();
            var uploadServiceBaseAddress = "http://uploadtoserver.azurewebsites.net/api/Files/Upload";

            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);
            var pathForDatabase = await httpResponseMessage.Content.ReadAsStringAsync();
            pathForDatabase = pathForDatabase.Substring(2, pathForDatabase.Length - 3);
            var ApathForDatabase = "http://uploadtoserver.azurewebsites.net/" + pathForDatabase;

            ImageT.PathToImage = ApathForDatabase;

        }
    }
}
