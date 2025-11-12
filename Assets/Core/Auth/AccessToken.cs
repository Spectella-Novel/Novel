using System;

namespace Core.Auth
{
    public class AccessToken : Token
    {
        public AccessToken(string value, DateTime expires) : base(value, expires)
        {
        }
    }
}
