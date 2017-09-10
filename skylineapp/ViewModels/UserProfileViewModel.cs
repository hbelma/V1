using skylineapp.Helpers;
using skylineapp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace skylineapp.ViewModels
{
    public class UserProfileViewModel : BaseViewModel
    {
        private ObservableCollection<User> user = new ObservableCollection<User>();
        private ObservableCollection<ImageTable> myImages = new ObservableCollection<ImageTable>();

        private UserManager userManager = UserManager.DefaultManager;
        private ImageManager imageManager = ImageManager.DefaultManager;

        public INavigation Navigation { get; set; }

        public ObservableCollection<User> User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        public ObservableCollection<ImageTable> MyImages
        {
            get
            {
                return myImages;
            }

            set
            {
                myImages = value;
                OnPropertyChanged("MyImages");
            }
        }

        public UserProfileViewModel(INavigation Navigation)
        {
            this.Navigation = Navigation;
            Task.Run(() => this.setProfileForCurrentUser()).Wait();


        }

        private async Task setProfileForCurrentUser()
        {
            string username = Settings.UserName;
            User = await userManager.GetUserByUsernameAsync(username);
            MyImages = await imageManager.GetPictureByUser(username);
        }

      
    }
}
