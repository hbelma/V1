using Plugin.Media;
using Plugin.Media.Abstractions;
using skylineapp.Helpers;
using skylineapp.Models;
using skylineapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using skylineapp.Services;
using Xamarin.Forms;
using System.Net.Http;

namespace skylineapp.ViewModels
{
    public class SignUpPageViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        private User user;
        private MediaFile mediaFile;

        private ICommand registerCommand;
        public ICommand RegisterCommand { get { return registerCommand; } }

        private ICommand addPhotoCommand;
        public ICommand AddPhotoCommand { get { return addPhotoCommand; } }

        private ICommand takePhotoCommand;
        public ICommand TakePhotoCommand { get { return takePhotoCommand; } }

        private UserManager userManager = UserManager.DefaultManager;

        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private bool enabled;
        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged("Enabled");
            }
        }


        public SignUpPageViewModel(INavigation navigation)
        {
            enabled = false;
            this.User = new User();
            this.Navigation = navigation;
            this.addPhotoCommand = new Command(async () => await ChoosePhoto());
            this.takePhotoCommand = new Command(async () => await TakePhoto());
            this.registerCommand = new Command(async () => await RegisterUser(this.User));
        }

        public async Task RegisterUser(User user)
        {
            
                await userManager.SaveUserAsync(user);
                await Navigation.PushAsync(new MainPage());
        }


        public async Task ChoosePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
                 return;
            
            mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if(mediaFile == null)
            {
                user.ProfilePhoto = "user.png";
                return;
            }

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(mediaFile.GetStream()), "\"file\"", $"\"{mediaFile.Path}\"");
            var httpClient = new HttpClient();
            var uploadServiceBaseAddress = "http://skylineappServerV2.azurewebsites.net/api/Files/Upload";

            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);
            var pathForDatabase = await httpResponseMessage.Content.ReadAsStringAsync();
            pathForDatabase = pathForDatabase.Substring(2, pathForDatabase.Length - 3);
            var ApathForDatabase = "http://skylineappServerV2.azurewebsites.net/" + pathForDatabase;

            user.ProfilePhoto = ApathForDatabase;

         
        }

        public async Task TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                return;

            var mediafile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (mediaFile == null)
                return;

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(mediaFile.GetStream()), "\"file\"", $"\"{mediaFile.Path}\"");
            var httpClient = new HttpClient();
            var uploadServiceBaseAddress = "http://skylineappServerV2.azurewebsites.net/api/Files/Upload";

            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);
            var pathForDatabase = await httpResponseMessage.Content.ReadAsStringAsync();
            pathForDatabase = pathForDatabase.Substring(2, pathForDatabase.Length - 3);
            var ApathForDatabase = "http://skylineappServerV2.azurewebsites.net/" + pathForDatabase;

            user.ProfilePhoto = ApathForDatabase;
        }
    }
}
