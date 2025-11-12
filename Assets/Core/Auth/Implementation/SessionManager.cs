using Core.Auth.Interfaces;
using System;


namespace Core.Auth.Implementation
{
    internal class SessionManager : ISessionManager
    {
        private Session _session;
        private IRefreshRepository _refreshRepository;

        public AccessToken AccessToken => _session.AccessToken;

        public bool IsLoggedIn => true;

        public void Init(Session session, IRefreshRepository refreshRepository)
        {
            _session = session;
            _refreshRepository = refreshRepository;
        }

        public void LogOut()
        {
            _session = null;
            _refreshRepository = null;
        }
    }
}
