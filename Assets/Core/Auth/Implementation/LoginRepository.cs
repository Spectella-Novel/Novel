using Core.Shared;
using Core.Http; // Предполагаемый namespace для HTTP-утилит
using System.Text;
using UnityEngine;
using System.Net.Http;
using Core.Http.Interfaces;
using System.Threading.Tasks;
using Core.Auth.Interfaces;
using System;

namespace Core.Auth.Implementation
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
            var payload = new LoginPayload();
            payload.password = credentials.Password;
            payload.login = credentials.Login;

            var jsonPayload = JsonUtility.ToJson(payload);
            var body = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResult<LoginResponse> response = await _httpService.Request()
                                                    .WithJsonBody(body)
                                                    .PostAsync<LoginResponse>(LoginUrl);

            if (!response.IsSuccess)
            {
                Debug.LogWarning($"Login failed: {response.Error}");
                return Result<Session>.Failure(response.Error);
            }
            var authResponse = response.Value;

            if (authResponse.status != "success")
            {
                Debug.LogWarning($"Login error: {authResponse.message}");
                return Result<Session>.Failure(authResponse.message);
            }
            var refreshTokenValue = response.GetHeader("refresh_token");
            if (refreshTokenValue == null)
            {
                return Result<Session>.Failure($"LoginRepository:Login(): Error {nameof(refreshTokenValue)} is empty");
            }
            RefreshToken refreshToken = new RefreshToken(refreshTokenValue, DateTime.Now.AddMonths(2));
            AccessToken accessToken = new AccessToken(authResponse.access_token, DateTime.Now.AddSeconds(int.Parse(authResponse.expires_in)));
            Session session = new SessionBase(refreshToken, accessToken); 

            return Result<Session>.Success(session);
        }


    }

    #region DTOs
    /// <summary>
    /// Payload model from /api/auth/login, /api/auth/refresh
    /// </summary>
    [System.Serializable]
    internal class LoginPayload
    {
        public string login;
        public string password;
    }

    /// <summary>
    /// Response model from /api/auth/login, /api/auth/refresh
    /// </summary>
    [System.Serializable]
    internal class LoginResponse
    {
        public string status;
        public string message;
        public string access_token;
        public string expires_in;
    }

    #endregion
}