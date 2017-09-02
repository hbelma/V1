using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using skylineapp.Helpers;
using skylineapp.Models;
using skylineapp.ViewModels;
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
        private ImageManager imageManager = ImageManager.DefaultManager;

        public static String kategorija;

        public ImageWrapLayoutPage(String category)
        {
            kategorija = category;
            BindingContext = new ImageWrapLayoutViewModel(this.Navigation, kategorija);          
            InitializeComponent();
        }        
    }
}