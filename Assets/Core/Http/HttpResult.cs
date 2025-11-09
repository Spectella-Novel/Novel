using Core.Shared;
using System.Collections.Generic;


namespace Core.Http.Interfaces
{
    public abstract class HttpResult<T> : Result<T>
    {
        Dictionary<string, string> headers;

        protected HttpResult(bool isSuccess, string error = null) : base(isSuccess, error)
        {
        }

        public void SetHeader(string key, string value)
        {
            headers[key] = value;
        }
        public string GetHeader(string key)
        {
            return headers[key];
        }
    }
}
