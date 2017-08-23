using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using skylineapp.Models;

namespace skylineapp.Services
{
    public class GoogleServices
    {
        public static readonly string ClientId = "307438969262-j283lkvfjqrmrq0ij1uqmkadi1234hmb.apps.googleusercontent.com";
        public static readonly string ClientSecret = "tHIM5VmrVnKwV73mD2Ws8d_z";
        public static readonly string RedirectUri = "https://www.youtube.com/";
        
        public async Task<string> GetAccessTokenAsync(string code)
        {
            var requestUrl =  "https://www.googleapis.com/oauth2/v4/token"
                + "?code=" + code
                + "&client_id=" + ClientId
                + "&client_secret=" + ClientSecret
                + "&redirect_uri=" + RedirectUri
                + "&grant_type=authorization_code";

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(requestUrl, null);
            var json = await response.Content.ReadAsStringAsync();
            var accessToken = JsonConvert.DeserializeObject<JObject>(json).Value<string>("access_token");
            return accessToken;
        }

        public async Task<GoogleProfile> GetGoogleUserProfileAsync(string accessToken)
        {
            var requestUrl = "https://www.googleapis.com/plus/v1/people/me"
                             + "?access_token=" + accessToken;

            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            var googleProfile = JsonConvert.DeserializeObject<GoogleProfile>(userJson);
            return googleProfile;
        }
    }
}
