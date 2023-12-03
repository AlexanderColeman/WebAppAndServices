using System.Net;

namespace FrontEndService.Manager.Interface
{
    public interface IHttpManager
    {
        Task<T> MakeGetAsync<T>(string baseUrl, string endpoint) where T : class;
        Task<T> MakePostAsync<T>(string baseUrl, string endpoint, object body) where T : class;
        Task<Q> MakePostAsync<T, Q>(string baseUrl, string endpoint, object body)
            where T : class
            where Q : class;
        Task<T> MakePutAsync<T>(string baseUrl, string endpoint, object body) where T : class;
        Task<Q> MakePutAsync<T, Q>(string baseUrl, string endpoint, object body)
           where T : class
           where Q : class;
        Task<HttpStatusCode> MakeDelete(string baseUrl, string endpoint);
    }
}
