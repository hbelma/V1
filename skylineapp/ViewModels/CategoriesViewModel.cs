﻿using Plugin.Media;
using Plugin.Media.Abstractions;
using skylineapp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using skylineapp.Views;


namespace skylineapp.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        private MediaFile mediaFile;

        private ICommand addPhotoCommand;
        public ICommand AddPhotoCommand { get { return addPhotoCommand; } }
        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories
        {
            get
            {
                return categories;
            }

            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }
        public CategoriesViewModel()
        {
            Categories = new ObservableCollection<Category>()
                {
                    new Category("animals.jpg", "Animals", "If you're brave enough to capture a tiger on it's hunting day show us. Or if you're a lazy bag, just post a photo of your pet :P"),
                    new Category("photoarhitecture.jpg" , "Architecture", "If you love to travel and look at the architectural wonders, check this out and see what's gonna be next on your travel list."),
                    new Category("hrana1.jpg", "Food", "Are you a foodie? Look at these photos of delicious delights. Maybe you're gonna have to make your own afterwards"),
                    new Category("nature.jpg", "Nature", "No one can get enough of wonderful nature. Trees, mountains, parks, flowers. Everything we love."),
                    new Category("nauka.jpg", "Science", "Something for geeks and science lovers. If you're a scientist or you just enjoy scientific experiments check these pics and tell us what you think."),
                    new Category("portret.jpg", "People", "You only take pictures of people? That's okay. We have so many great photos to show you."),
                    new Category("urban.jpg", "Urban", "Look at these wonderful places. Share photo of your city to show people what they're missing...")

                };
        }

        public CategoriesViewModel(INavigation navigation)
        {
            Navigation.PushAsync(new AddImage());
        }

        /*public async Task ChoosePhoto()
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

            var uploadServiceBaseAddress = "http://uploadtoserver.azurewebsites.net/api/Files/Upload";

            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

            var pathForDatabase = await httpResponseMessage.Content.ReadAsStringAsync();
            pathForDatabase = pathForDatabase.Substring(2, pathForDatabase.Length - 3);
            var ApathForDatabase = "http://uploadtoserver.azurewebsites.net/" + pathForDatabase;
            if (mediaFile == null)
                return;
            user.ProfilePhoto = ApathForDatabase;
        }*/
    }
}
