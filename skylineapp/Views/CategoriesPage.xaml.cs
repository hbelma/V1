using skylineapp.Models;
using skylineapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace skylineapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        ObservableCollection<Category> Categories = new ObservableCollection<Category>()
                {
                    new Category("animals.jpg", "Animals", "If you're brave enough to capture a tiger on it's hunting day show us. Or if you're a lazy bag, just post a photo of your pet :P"),
                    new Category("photoarhitecture.jpg" , "Architecture", "If you love to travel and look at the architectural wonders, check this out and see what's gonna be next on your travel list."),
                    new Category("hrana1.jpg", "Food", "Are you a foodie? Look at these photos of delicious delights. Maybe you're gonna have to make your own afterwards"),
                    new Category("nature.jpg", "Nature", "No one can get enough of wonderful nature. Trees, mountains, parks, flowers. Everything we love."),
                    new Category("nauka.jpg", "Science", "Something for geeks and science lovers. If you're a scientist or you just enjoy scientific experiments check these pics and tell us what you think."),
                    new Category("portret.jpg", "People", "You only take pictures of people? That's okay. We have so many great photos to show you."),
                    new Category("urban.jpg", "Urban", "Look at these wonderful places. Share photo of your city to show people what they're missing...")

                };

        public CategoriesPage()
        {
            InitializeComponent();

            BindingContext = new CategoriesViewModel();

        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            ListView list = sender as ListView;
            Category kategorija = list.SelectedItem as Category;

            if (kategorija != null)
            {
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Problems", "Problems", "OK");
            }
        }
    }
}