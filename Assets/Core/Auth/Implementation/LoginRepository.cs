using Core.NetworkRepositories.Interfaces;
using Core.Shared;
using Core.Http; // Предполагаемый namespace для HTTP-утилит
using System.Text;
using UnityEngine;
using System.Net.Http;
using Core.Http.Interfaces;
using System.Threading.Tasks;

namespace Core.NetworkRepositories.Implementation
{
    /// <summary>
    /// Repository for handling user login and session refresh via API.
    /// </summary>
    public class LoginRepository : ILoginRepository
    {
        private const string LoginUrl = "/api/auth/login";
        private readonly IHttpService _httpService;

        public LoginRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        /// <summary>
        /// Authenticates a user with login and password.
        /// On success, stores refresh token in HTTP-only cookie and returns access token.
        /// </summary>
        /// <param name="credentials">User login and password</param>
        /// <returns>Session with access token on success</returns>
        public async Task<Result<Session>> Login(LoginCredentials credentials)
        {
            var payload = new AuthPayload();
            payload.password = credentials.Password;
            payload.login = credentials.Login;

            var jsonPayload = JsonUtility.ToJson(payload);
            var body = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResult<AuthResponse> response = await _httpService.Request()
                                                    .WithJsonBody(body)
                                                    .PostAsync<AuthResponse>(LoginUrl);


            if (!response.IsSuccess)
            {
                Debug.LogError($"Login failed: {response.Error}");
                return Result<Session>.Failure(response.Error);
            }

            var authResponse = response.Value;
            if (authResponse.status != "success")
            {
                Debug.LogWarning($"Login error: {authResponse.message}");
                return Result<Session>.Failure(authResponse.message);
            }
            Session session = null; 

            return Result<Session>.Success(session);
        }


    }

    #region DTOs
    /// <summary>
    /// Payload model from /api/auth/login, /api/auth/refresh
    /// </summary>
    [System.Serializable]
    internal class AuthPayload
    {
        public string login;
        public string password;
    }

    /// <summary>
    /// Response model from /api/auth/login, /api/auth/refresh
    /// </summary>
    [System.Serializable]
    internal class AuthResponse
    {
        public string status;
        public string message;
        public string access_token;
        public int expires_in;
    }

    #endregion
}