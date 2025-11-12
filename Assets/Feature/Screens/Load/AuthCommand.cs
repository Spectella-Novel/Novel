using Core.Auth;
using Core.Auth.Implementation;
using Core.Auth.Interfaces;
using Core.Shared;
using Core.Storage.Interfaces;
using System.Threading.Tasks;

namespace Novel.Feature.Screens.Load
{
    /// <summary>
    /// Handles user authentication on app start.
    /// Tries to restore session using saved credentials.
    /// If none exist, registers a new anonymous user.
    /// </summary>
    internal class AuthCommand : ICommand
    {
        private readonly IStorage _storage;
        private readonly ILoginRepository _loginRepository;
        private readonly IRegisterRepository _registerRepository;
        private readonly IRefreshRepository _refreshRepository;
        private readonly ISessionManager _sessionManager;

        public AuthCommand(IStorage storage,
                ILoginRepository loginRepository,
                IRegisterRepository registerRepository,
                ISessionManager sessionManager,
                IRefreshRepository refreshRepository)
        {
            _storage = storage;
            _loginRepository = loginRepository;
            _registerRepository = registerRepository;
            _sessionManager = sessionManager;
            _refreshRepository = refreshRepository;
        }

        /// <summary>
        /// Executes authentication:
        /// 1. Attempts to restore saved credentials.
        /// 2. If found — tries to refresh session, then regular login if refresh fails.
        /// 3. If no credentials or login fails — registers a new anonymous user.
        /// </summary>
        /// <returns>Success or failure result</returns>
        public async Task<Result> Execute()
        {
            Result<Session> sessionResult;

            // Try to load previously saved session
            sessionResult = await _storage.Restore<Session>("Session");
            if (sessionResult.IsSuccess && sessionResult.Value != default(Session))
            {

                // If refresh failed, attempt regular login
                sessionResult = await _refreshRepository.Refresh(sessionResult.Value);
                if (sessionResult.IsSuccess && sessionResult.Value != default(Session))
                {
                    _sessionManager.Init(sessionResult.Value, _refreshRepository);
                    return Result.Success();
                }

            }

            // Try to load previously saved login credentials
            Result<LoginCredentials> result = await _storage.Restore<LoginCredentials>("Credentials");
            
            if (result.IsSuccess && result.Value != default(LoginCredentials))
            {

                // If refresh failed, attempt regular login
                sessionResult = await _loginRepository.Login(result.Value);
                if (sessionResult.IsSuccess && sessionResult.Value != default(Session))
                {
                    _sessionManager.Init(sessionResult.Value, _refreshRepository);
                    return Result.Success();
                }

                // Both methods failed
                return Result.Failure("Failed to authenticate with saved credentials.");
            }
            // No saved credentials — register a new anonymous user
            var login = StringUtils.GenerateRandomString();
            var password = StringUtils.GenerateRandomString();
            LoginCredentials credentials = new LoginCredentials(login, password);

            sessionResult = await _registerRepository.Register(credentials);
            if (sessionResult.IsSuccess && sessionResult.Value != default(Session))
            {
                _sessionManager.Init(sessionResult.Value, _refreshRepository);

                // Persist credentials for next launch
                Result saveResult = await _storage.Save("Credentials", credentials);
                if (!saveResult.IsSuccess)
                {
                    return saveResult; // Return save error
                }

                return Result.Success();
            }

            // Registration failed — propagate the error
            return sessionResult;
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <returns>Success result after logout</returns>
        public async Task<Result> Undo()
        {
            _sessionManager.LogOut();
            return Result.Success();
        }
    }
}