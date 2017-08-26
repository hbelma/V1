using Newtonsoft.Json;
using skylineapp.Models;
using System;
using System.Collections.Generic;
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
        public ImageWrapLayoutPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var images = await GetImageListAsync();

            foreach (var photo in images.Photos)
            {
                var image = new Xamarin.Forms.Image
                {
                    Source = ImageSource.FromUri(new Uri(photo + string.Format("?width={0}&height={0}&mode=max", Xamarin.Forms.Device.OnPlatform(240, 240, 120))))
                };

                wrapLayout.Children.Add(image);
            }
        }

        async Task<ImageList> GetImageListAsync()
        {
            var requestUri = "https://docs.xamarin.com/demo/stock.json";
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(requestUri);
                return JsonConvert.DeserializeObject<ImageList>(result);
            }
        }
    }
}