using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.MobileServices;
using Foundation;
using UIKit;
using Plugin.Media;

namespace skylineapp.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static async void  Main (string[] args)
		{
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            await CrossMedia.Current.Initialize();
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}

