using skylineapp.Helpers;
using skylineapp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace skylineapp.Behavior
{
    public class UsernameValidator : Behavior<Entry>
    {
        const string usernameRegex = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";


        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NumberValidator), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
        private UserManager userManager = UserManager.DefaultManager;

        ObservableCollection<User> users = new ObservableCollection<User>();

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }


        async void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            users = await userManager.GetUserByUsernameAsync(e.NewTextValue);
            IsValid = (Regex.IsMatch(e.NewTextValue, usernameRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250))) && users.Count == 0;
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;

        }
    }
}
