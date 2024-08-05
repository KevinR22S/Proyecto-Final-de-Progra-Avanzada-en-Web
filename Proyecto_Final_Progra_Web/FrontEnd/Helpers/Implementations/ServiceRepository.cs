using FrontEnd.Helpers.Intefaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        public HttpClient Client { get; set; }

        public ServiceRepository(HttpClient client, IConfiguration configuration)
        {
            Client = client;
            string baseUrl = "http://localhost:5190";
            Client.BaseAddress = new Uri(baseUrl);
        }

        public async Task<HttpResponseMessage> GetResponse(string url)
        {
            try
            {
                return await Client.GetAsync(url);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error during GET request", ex);
            }
        }

        public async Task<HttpResponseMessage> PutResponse(string url, object model)
        {
            try
            {
                return await Client.PutAsJsonAsync(url, model);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error during PUT request", ex);
            }
        }

        public async Task<HttpResponseMessage> PostResponse(string url, object model)
        {
            try
            {
                return await Client.PostAsJsonAsync(url, model);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error during POST request", ex);
            }
        }

        public async Task<HttpResponseMessage> DeleteResponse(string url)
        {
            try
            {
                return await Client.DeleteAsync(url);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error during DELETE request", ex);
            }
        }
    }
}
