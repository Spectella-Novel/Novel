using Core.NetworkRepositories;
using Core.NetworkRepositories.Implementation;
using Core.NetworkRepositories.Interfaces;
using Core.Shared;

namespace Core.Auth.Implementation
{
    internal class RegisterRepository : IRegisterRepository
    {
        public Result<Session> Register(LoginCredentials credentials)
        {
            throw new System.NotImplementedException();
        }
    }
}
