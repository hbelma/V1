using Plugin.Media;
using Plugin.Media.Abstractions;
using skylineapp.Helpers;
using skylineapp.Models;
using skylineapp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace skylineapp.ViewModels
{
    public class ImageWrapLayoutViewModel
    {
        public INavigation Navigation { get; set; }
        public string cat;

        private ICommand addPhotoCommand;
        public ICommand AddPhotoCommand { get { return addPhotoCommand; } }


        public ImageWrapLayoutViewModel(INavigation nav, String category) {
            cat = category;
            this.Navigation = nav;
            this.addPhotoCommand = new Command(async () => await GoToUploadPhoto());
        }


        public async Task GoToUploadPhoto()
        {
            await this.Navigation.PushAsync(new UploadPhotoPage(cat));

        }
    }


}
