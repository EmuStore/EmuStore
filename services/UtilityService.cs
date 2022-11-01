using System.Net.Http.Headers;
using System.Net.Http;
using System;
using Newtonsoft.Json;

namespace EmuStore.Services
{
    public static class UtilityService
    {
        public static HttpClient GetHttpClient(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                token
            );
            client.BaseAddress = new Uri("http://localhost:3080");
            return client;
        }

        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
