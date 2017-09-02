using skylineapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace skylineapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadPhotoPage : ContentPage
    {

        public UploadPhotoPage(String category)
        {
            BindingContext = new UploadPhotoViewModel(this.Navigation, category);
            InitializeComponent();
        }
    }
}