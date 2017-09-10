using skylineapp.Helpers;
using skylineapp.Models;
using skylineapp.Services;
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
using Xamarin.Forms;

namespace skylineapp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        private ICommand loginCommand;
        public ICommand LoginCommand { get { return loginCommand; } }

        private ICommand signUpCommand;
        public ICommand SignUpCommand { get { return signUpCommand; } }

        private UserManager userManager = UserManager.DefaultManager;
        private bool wrongUsernameOrPassword;

        public MainPageViewModel(INavigation navigation)
        {
            this.WrongUsernameOrPassword = false;
            this.Navigation = navigation;
            signUpCommand = new Command(async () => await GoToSignUpPage());
            loginCommand = new Command(async () => await LoginUser());
        }

        public async Task LoginUser()
        {
            ObservableCollection<User> users = await userManager.GetUserByUsernameAsync(Username);

            //if user is registered start session and go to categoriespage
            if (users.Count == 1)
            {
                Settings.UserName = Username;
                Settings.UserPassword = Password;
                await Navigation.PushAsync(new CategoriesPage());
            }

            //notify user
            else
            {                
                WrongUsernameOrPassword = true;
            }
        }


        public async Task GoToSignUpPage()
        {
            await Navigation.PushAsync(new SignUpPage());
        }

   
        private FacebookProfile _facebookProfile;

        public FacebookProfile FacebookProfile
        {
            get { return _facebookProfile; }
            set
            {
                _facebookProfile = value;
                OnPropertyChanged();
            }
        }

        public async Task SetFacebookUserProfileAsync(string accessToken)
        {
            var facebookServices = new FacebookServices();
            FacebookProfile = await facebookServices.GetFacebookProfileAsync(accessToken);

            User user = new User();

            user.FirstName = FacebookProfile.FirstName;
            user.LastName = FacebookProfile.LastName;
            user.Username = FacebookProfile.Name;
            user.ProfilePhoto = FacebookProfile.Picture.Data.Url;
            user.Gender = FacebookProfile.Gender;
            user.Age = FacebookProfile.AgeRange.Min;

            ObservableCollection<User> userIsInSystem = await userManager.GetUserByUsernameAsync(user.Username);

            if (userIsInSystem.Count == 0)
            {
                Settings.UserName = user.Username;
                Settings.UserPassword = "FbUser";
                await userManager.SaveUserAsync(user);
            }
      
        }

     
        private string username;
        public string Username
        {
            set
            {
                SetProperty(ref username, value);
            }
            get
            {
                return username;
            }
        }

        private string password;
        public string Password
        {
            set
            {
                SetProperty(ref password, value);
            }
            get
            {
                return password;
            }
        }

        public bool WrongUsernameOrPassword { get { return wrongUsernameOrPassword; } set { wrongUsernameOrPassword = value; OnPropertyChanged("WrongUsernameOrPassword"); } }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }


    }
}

