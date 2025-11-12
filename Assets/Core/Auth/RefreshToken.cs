using System;

namespace Core.Auth
{
    public class RefreshToken : Token
    {
        public RefreshToken(string value, DateTime expires) : base(value, expires)
        {
        }
    }
}
