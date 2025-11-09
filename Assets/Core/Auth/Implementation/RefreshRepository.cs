
using Core.NetworkRepositories;
using Core.NetworkRepositories.Interfaces;
using Core.Shared;

namespace Core.Auth.Implementation
{
    public class RefreshRepository : IRefreshRepository
    {
        Result<Session> IRefreshRepository.Refresh(Session session)
        {
            throw new System.NotImplementedException();
        }
    }
}
