using Core.Auth.Implementation;
using Core.Http.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;
using static Core.Auth.Implementation.RegisterRepository;

namespace Core.Http.Implementation
{
    public class MockHttpService : IHttpService
    {
        public IHttpBuilder Request()
        {
            return new MockHttpBuilder();
        }
    }
    public class MockHttpBuilder : IHttpBuilder
    {
        public async Task<HttpResult<T>> DeleteAsync<T>(string url)
        {
            await Task.Delay(3000);
            return HttpResult<T>.Success(default(T));
        }

        public async Task<HttpResult<T>> GetAsync<T>(string url)
        {
            await Task.Delay(3000);
            return HttpResult<T>.Success(default(T));
        }

        public async Task<HttpResult<T>> PostAsync<T>(string url)
        {
            if (typeof(T) == typeof(RegisterResponse))
            {
                var response = new RegisterResponse();
                response.status = "success";
                response.access_token = "access_token";
                response.expires_in = "3600";
                var result = HttpResult<RegisterResponse>.Success(response);
                result.SetHeader("refresh_token", "refresh_token");
                return result as HttpResult<T>;
            }
            if (typeof(T) == typeof(LoginResponse))
            {
                var response = new LoginResponse();
                response.status = "success";
                response.access_token = "access_token";
                response.expires_in = "3600";
                var result = HttpResult<LoginResponse>.Success(response);
                result.SetHeader("refresh_token", "refresh_token");
                return result as HttpResult<T>;
             }
            await Task.Delay(3000);
            return HttpResult<T>.Success(default(T));
        }

        public async Task<HttpResult<T>> PutAsync<T>(string url)
        {
            await Task.Delay(3000);
            return HttpResult<T>.Success(default(T));
        }

        public IHttpBuilder WithBearerToken(string token)
        {
            return this;
        }

        public IHttpBuilder WithHeader(string name, string value)
        {
            return this;
        }

        public IHttpBuilder WithJsonBody(object data)
        {
            return this;
        }
    }
}
