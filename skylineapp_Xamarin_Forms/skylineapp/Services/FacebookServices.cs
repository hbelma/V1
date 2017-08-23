using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using skylineapp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace skylineapp.Services
{
    public class FacebookServices
    {
        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.7/me/?fields=name,picture.type(large),locale,link,cover.type(large),age_range,devices,email,first_name,last_name,gender,is_verified,id&access_token="
                + accessToken;

            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);
            return facebookProfile;
        }
    }
}
