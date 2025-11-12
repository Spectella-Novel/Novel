
using Core.Auth.Interfaces;
using Core.Shared;
using System.Threading.Tasks;

namespace Core.Auth.Implementation
{
    public class RefreshRepository : IRefreshRepository
    {
        public async Task<Result<Session>> Refresh(Session session)
        {
            await Task.Delay(3000);
            return Result<Session>.Success( new MockSession());
        }
    }
}
