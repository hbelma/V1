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
    public class SignUpPageViewModel
    {
        public INavigation Navigation { get; set; }
        private User user;
        private MediaFile mediaFile;
        private ImageUploadService imageService;

        private ICommand registerCommand;
        public ICommand RegisterCommand { get { return registerCommand; } }

        private ICommand addPhotoCommand;
        public ICommand AddPhotoCommand { get { return addPhotoCommand; } }


        private UserManager userManager = UserManager.DefaultManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public SignUpPageViewModel(INavigation navigation)
        {
            this.User = new User();
            this.Navigation = navigation;
            this.addPhotoCommand = new Command(async () => await ChoosePhoto());
            this.registerCommand = new Command(async () => await RegisterUser(this.User));
        }

        public async Task RegisterUser(User user)
        {
            ObservableCollection<User> useRs = await userManager.GetUserByUsernameAsync(user.Username);
            ObservableCollection<User> useRs2 = await userManager.GetUserByEmailAsync(user.Email);


            if (useRs.Count == 0 && useRs2.Count == 0)
            {
                
                await userManager.SaveTaskAsync(user);
                await Navigation.PushAsync(new CategoriesPage());
            }
            else
            {
                MessagingCenter.Send(this, "MyAlertName", "My actual alert content, or an object if you want");
            }

        }

        private bool RegisterUserInFunction()
        {

            return true;
        }

        public async Task ChoosePhoto()
        {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    return;
                }

                mediaFile = await CrossMedia.Current.PickPhotoAsync();
                var content = new MultipartFormDataContent();

                content.Add(new StreamContent(mediaFile.GetStream()),
                    "\"file\"",
                    $"\"{mediaFile.Path}\"");

                var httpClient = new HttpClient();

                var uploadServiceBaseAddress = "http://localhost:2204/api/Files/Upload";

                var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

                var pathForDatabase = await httpResponseMessage.Content.ReadAsStringAsync();

            var ApathForDatabase = "http://uploadtoserver.azurewebsites.net/" + pathForDatabase;
                if (mediaFile == null)
                    return;

                user.ProfilePhoto = mediaFile.Path;
        }


    }
}
