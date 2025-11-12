using Core.Auth.Implementation;
using Core.Shared;
using System;
using System.Collections.Generic;


namespace Core.Http.Interfaces
{
    public class HttpResult<T> : Result<T>
    {
        Dictionary<string, string> headers;

        public HttpResult(bool isSeccess, T value, string error = null) : base(isSeccess, value, error)
        {
            headers = new Dictionary<string, string>();
        }

        public void SetHeader(string key, string value)
        {
            headers[key] = value;
        }
        public string GetHeader(string key)
        {
            return headers[key];
        }

        public static new HttpResult<T> Success(T value) => new(true, value);
        public static new HttpResult<T> Failure(string error) => new(false, default, error);


    }
}
