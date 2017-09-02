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
        public CategoriesPage()
        {
            BindingContext = new CategoriesViewModel();
            InitializeComponent();
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; 
            }

            ListView list = sender as ListView;
            Category kategorija = list.SelectedItem as Category;

            if (kategorija != null)
            {
                Navigation.PushAsync(new ImageWrapLayoutPage(kategorija.Title));
            }
            else
            {
                DisplayAlert("Problems", "Problems", "OK");
            }
        }
    }
}