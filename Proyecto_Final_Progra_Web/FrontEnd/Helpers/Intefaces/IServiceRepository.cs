namespace FrontEnd.Helpers.Intefaces
{
    public interface IServiceRepository
    {
        Task<HttpResponseMessage> GetResponse(string url);
        Task<HttpResponseMessage> PutResponse(string url, object model);
        Task<HttpResponseMessage> PostResponse(string url, object model);
        Task<HttpResponseMessage> DeleteResponse(string url);
    }
}
