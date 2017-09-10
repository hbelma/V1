using skylineapp.Views;
using System;

using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using DLToolkit.Forms.Controls;

namespace skylineapp
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new MainPage())
            {
                Title = "Skyline"
            };
        }

        public static Action<string> PostSuccessFacebookAction { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

