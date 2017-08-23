﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using skylineapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace skylineapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacebookProfilePage : ContentPage
    {
        /// Get a new ClientId from:
        /// https://developers.facebook.com/apps/

        private string ClientId = "106421706735463";

        public FacebookProfilePage()
        {
            InitializeComponent();
            
            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + ClientId
                + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";
            
            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;
            Content = webView;
        }
        
        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            ///Access URL because URL contains informations about AccessToken
            var accessToken = ExtractAccessTokenFromUrl(e.Url);            
            if (accessToken != "")
            {
                var vm = BindingContext as FacebookViewModel;
                await vm.SetFacebookUserProfileAsync(accessToken);
                Content = MainStackLayout;
            }
        }
        
        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");                

             //   if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
              //  {
                //    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
               // }
                
                var accessToken = at.Remove(at.IndexOf("&expires_in="));
                return accessToken;
            }
            return string.Empty;
        }
    }
}