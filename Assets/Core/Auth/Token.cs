using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Auth
{
    public class Token
    {
        public string Value;
        public DateTime Expires;

        public Token(string value, DateTime expires)
        {
            Value = value;
            Expires = expires;
        }
    }
}
