using Core.Shared;
using System.Threading.Tasks;
using Core.Auth.Interfaces;
using Core.Auth.Implementation;

namespace Core.Auth
{
    interface ILoginRepository
    {
        public Task<Result<Session>> Login(LoginCredentials credentials);
    }
    interface IRegisterRepository
    {
        public Task<Result<Session>> Register(LoginCredentials credentials);
    }
    interface IRefreshRepository
    {
        public Task<Result<Session>> Refresh(Session session);
    }
}