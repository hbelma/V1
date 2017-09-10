using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;
using Plugin.Settings.Abstractions;


namespace skylineapp.Helpers
{
     public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

    
        private const string UserNameKey = "username_key";
        private static readonly string UserNameDefault = string.Empty;

        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserNameKey, value);
            }
        }

        private const string UserPasswordKey = "userpassword_key";
        private static readonly string UserPasswordDefault = string.Empty;

        public static string UserPassword
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserPasswordKey, UserPasswordDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserPasswordKey, value);
            }
        }
    }
}
