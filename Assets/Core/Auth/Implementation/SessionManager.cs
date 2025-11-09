using Core.NetworkRepositories;
using Core.NetworkRepositories.Interfaces;
using System;


namespace Core.Auth.Implementation
{
    internal class SessionManager : ISessionManager
    {
        public AccessToken AccessToken => throw new NotImplementedException();

        public bool IsLoggedIn => throw new NotImplementedException();

        public void Init(Session session, IRefreshRepository refreshRepository)
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
