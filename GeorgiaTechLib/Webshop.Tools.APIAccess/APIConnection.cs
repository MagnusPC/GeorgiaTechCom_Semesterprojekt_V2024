using System.Net.Http.Json;

namespace Webshop.Tools.APIAccess
{
    public class APIConnection
    {
        HttpClient client;
        string baseUrl;

        public APIConnection(string baseUrl)
        {
            client = new HttpClient();
            this.baseUrl = baseUrl;
        }

        public async Task PostRequest<T>(string endpoint, T Payload)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"{baseUrl}/{endpoint}", Payload);
            response.EnsureSuccessStatusCode();
        }

        public async Task<T?> GetRequest<T>(string endpoint)
        {
            T? result = default;
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{endpoint}");

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<T>();
            }

            return result;
        }
    }
}
