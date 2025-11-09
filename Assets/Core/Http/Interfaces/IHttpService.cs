using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Http.Interfaces
{
    public interface IHttpService
    {
        IHttpBuilder Request();

    }
    public interface IHttpBuilder
    {
        IHttpBuilder WithHeader(string name, string value);
        IHttpBuilder WithBearerToken(string token);
        IHttpBuilder WithJsonBody(object data);
        Task<HttpResult<T>> GetAsync<T>(string url);
        Task<HttpResult<T>> PostAsync<T>(string url);
        Task<HttpResult<T>> PutAsync<T>(string url);
        Task<HttpResult<T>> DeleteAsync<T>(string url);
    }


}
