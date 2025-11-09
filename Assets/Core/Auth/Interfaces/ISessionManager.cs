using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NetworkRepositories.Interfaces
{
    interface ISessionManager
    { 
        AccessToken AccessToken { get; }
        bool IsLoggedIn { get; }

        void Init(Session session, IRefreshRepository refreshRepository);
        void LogOut();
    }
}
