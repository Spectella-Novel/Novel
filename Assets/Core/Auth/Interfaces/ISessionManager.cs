namespace Core.Auth.Interfaces
{
    interface ISessionManager
    { 
        AccessToken AccessToken { get; }
        bool IsLoggedIn { get; }

        void Init(Session session, IRefreshRepository refreshRepository);
        void LogOut();
    }
}
