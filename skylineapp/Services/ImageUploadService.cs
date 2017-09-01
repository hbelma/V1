using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Plugin.Media.Abstractions;

namespace skylineapp.Services
{
    public class ImageUploadService
    {
        public async void UploadFile(MediaFile _mediaFile)
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