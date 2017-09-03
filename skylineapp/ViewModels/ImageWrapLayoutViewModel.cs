using Plugin.Media;
using Plugin.Media.Abstractions;
using skylineapp.Helpers;
using skylineapp.Models;
using skylineapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace skylineapp.ViewModels
{
    public class ImageWrapLayoutViewModel : BaseViewModel
    {
        private ObservableCollection<ImageTable> images = new ObservableCollection<ImageTable>();
        private ImageManager imageManager = ImageManager.DefaultManager;
        private UserManager userManager = UserManager.DefaultManager;

        public INavigation Navigation { get; set; }
        public string cat;
        private String userProfilePhoto;

        private ICommand addPhotoCommand;
        public ICommand AddPhotoCommand { get { return addPhotoCommand; } }

        public ObservableCollection<ImageTable> Images
        {
            get
            {
                return images;
            }

            set
            {
                images = value;
                OnPropertyChanged();
            }
        }

         public String UserProfilePhoto
        {
            get => userProfilePhoto;
            set
            {
                userProfilePhoto = value;
                OnPropertyChanged("UserProfilePhoto");
            }
        }

        public ImageWrapLayoutViewModel(INavigation nav, String category) {
            cat = category;
            setImages();
            this.Navigation = nav;
            this.addPhotoCommand = new Command(async () => await GoToUploadPhoto());

        }


        private async void setImages()
        {
            Images = await imageManager.GetPictureByCategoryh(cat);
        }


        public async Task GoToUploadPhoto()
        {
            await this.Navigation.PushAsync(new UploadPhotoPage(cat));
        }
    }


}
