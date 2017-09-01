using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using skylineapp.Helpers;
using skylineapp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace skylineapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageWrapLayoutPage : ContentPage
    {
        private MediaFile mediaFile;
        private ImageTable image;
        private ImageManager imageManager = ImageManager.DefaultManager;
        private UserManager userManager = UserManager.DefaultManager;

        public ImageWrapLayoutPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var images = await GetImageListAsync();

            foreach (var photo in images)
            {
                var image = new Xamarin.Forms.Image
                {
                    Source = ImageSource.FromUri(new Uri(photo + string.Format("?width={0}&height={0}&mode=max", Xamarin.Forms.Device.OnPlatform(240, 240, 120))))
                };

                wrapLayout.Children.Add(image);
            }
        }

        async Task<string[]> GetImageListAsync()
        {
            
            List<string> terms = new List<string>();
            ObservableCollection<User> useRs = await userManager.GetUser();
            using (var client = new HttpClient())
            {
                for(int i = 0; i < useRs.Count; i++)
                {
                    terms.Add(useRs[i].ProfilePhoto);
                }
                string[] test = terms.ToArray();
                /*var help = string.Join("", test);
                var result = await client.GetStringAsync(help);
                var JSONSomething = JsonConvert.DeserializeObject<ImageList>(result);*/
                //var andrej = "http://uploadtoserver.azurewebsites.net/Uploads/Darren%20Korb%20-%20Transistor%20Original%20Soundtrack%20-%20cover.png";
                //JSONSomething.Photos[0] = andrej;
                return test;
            }
            /*var requestUri = "https://docs.xamarin.com/demo/stock.json";
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(requestUri);
                var JSONSomething = JsonConvert.DeserializeObject<ImageList>(result);
                return JSONSomething;
            }*/
        }

        private async void PickPhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No PickPhoto", ":( No PickPhoto available.", "OK");
                return;
            }

            mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (mediaFile == null)
                return;

            
        }

        private async void UploadFile_Clicked(object sender, EventArgs e)
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
            image.PathToImage = ApathForDatabase;
            await imageManager.SaveTaskAsync(image);
            if (mediaFile == null)
                return;
        }


    }
}