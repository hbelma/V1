using skylineapp.Behavior;
using skylineapp.Interfaces;
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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            BindingContext = new SignUpPageViewModel(this.Navigation);

            InitializeComponent();
        }


    }
}