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
    public class MainPageViewModel 
    {
        public INavigation Navigation { get; set; }

        private ICommand loginCommand;
        public ICommand LoginCommand { get { return loginCommand; } }

        private ICommand signUpCommand;
        public ICommand SignUpCommand { get { return signUpCommand; } }

        private ICommand loginWithFBCommand;
        public ICommand LoginWithFBCommand { get { return loginWithFBCommand; } }

        private UserManager userManager = UserManager.DefaultManager;

        public MainPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            signUpCommand = new Command(async () => await GoToSignUpPage());
            loginCommand = new Command(async () => await LoginUser());
        }

        public async Task GoToSignUpPage()
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        public async Task SignIn()
        {
            // To Do Login Function, check with db if user exits and if password is correct
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
            user.Email = FacebookProfile.Id;
            /* Add moew user info*/
            ObservableCollection<User> userIsInSystem = await userManager.GetUserByEmailAsync(FacebookProfile.Id);

            if (userIsInSystem.Count == 0)
            {
                await userManager.SaveTaskAsync(user);
            }
            else
            {   /*Get usr info that is to say user ID which is unique usrIsInSystem[0].Id*/

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoginUser()
        {
            ObservableCollection<User> users = await userManager.GetUserByUsernameAsync(Username);
            if (users.Count == 1) {

                    if ((Settings.UserName == string.Empty && Settings.UserPassword == string.Empty) ||
                        ((Settings.UserName != string.Empty && Settings.UserName != Username) && (Settings.UserPassword != string.Empty && Settings.UserPassword != Password)))
                    {
                        Settings.UserName = Username;
                        Settings.UserPassword = Password;
                    }

                    await Navigation.PushAsync(new CategoriesPage());
                
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

