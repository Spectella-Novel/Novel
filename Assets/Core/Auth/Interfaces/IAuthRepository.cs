using Core.Shared;
using Core.NetworkRepositories.Interfaces;
using Core.NetworkRepositories.Implementation;
using System.Threading.Tasks;

namespace Core.NetworkRepositories
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