using Core.NetworkRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NetworkRepositories.Interfaces
{
    public abstract class Session
    {
        public RefreshToken RefreshToken { get; }
        public AccessToken AccessToken { get; }
        public abstract bool IsExpired();
    }
}
