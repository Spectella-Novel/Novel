using System;

namespace Core.Auth.Interfaces
{
    public abstract class Session
    {
        protected Session() { }
        protected Session(RefreshToken refreshToken, AccessToken accessToken)
        {
            RefreshToken = refreshToken;
            AccessToken = accessToken;
        }

        public RefreshToken RefreshToken { get; protected set; }
        public AccessToken AccessToken { get; protected set; }
        public abstract bool IsExpired();
    }
    public class SessionBase : Session
    {
        protected SessionBase():base() { }
        public SessionBase(RefreshToken refreshToken, AccessToken accessToken) : base(refreshToken, accessToken)
        {
        }

        public override bool IsExpired()
        {
            return AccessToken.Expires > DateTime.Now;
        }
    }
    public class MockSession : SessionBase
    {
        public MockSession(): base()
        {
            RefreshToken = new RefreshToken("Mock", DateTime.Now.AddYears(1));
            AccessToken = new AccessToken("MockAccess", DateTime.Now.AddDays(14));
        }
        public override bool IsExpired()
        {
            return AccessToken.Expires > DateTime.Now;
        }
    }
}
