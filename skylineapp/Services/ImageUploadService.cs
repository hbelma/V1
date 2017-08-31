﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;


namespace skylineapp.Droid
{
    public class ImageUploadService
    {
        private async void UploadFile()
        {
            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(_mediaFile.GetStream()),
                "\"file\"",
                $"\"{_mediaFile.Path}\"");

            var httpClient = new HttpClient();

            var uploadServiceBaseAddress = "http://uploadtoserver.azurewebsites.net/api/Files/Upload";

            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

            var pathForDatabase = await httpResponseMessage.Content.ReadAsStringAsync();

            var andrej = "AndrejBelmaVol69";
        }

    }
}