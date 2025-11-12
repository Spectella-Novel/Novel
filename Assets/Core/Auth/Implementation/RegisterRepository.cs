using Core.Auth.Interfaces;
using Core.Http.Interfaces;
using Core.Shared;
using System.Net.Http;
using System.Text;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Auth.Implementation
{
    public class RegisterRepository : IRegisterRepository
    {
        const string RegisterURL = "/api/auth/register";
        private IHttpService _httpService;


        public RegisterRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }


        public async Task<Result<Session>> Register(LoginCredentials credentials)
        {
            var payload = new RegisterPayload();
            payload.password = credentials.Password;
            payload.login = credentials.Login;

            var jsonPayload = JsonUtility.ToJson(payload);
            var body = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResult<RegisterResponse> response = await _httpService.Request()
                                                    .WithJsonBody(body)
                                                    .PostAsync<RegisterResponse>(RegisterURL);

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
            if (refreshTokenValue != null)
            {
                return Result<Session>.Failure($"LoginRepository:Login(): Error {nameof(refreshTokenValue)} is empty");
            }
            RefreshToken refreshToken = new RefreshToken(refreshTokenValue, DateTime.Now.AddMonths(2));
            AccessToken accessToken = new AccessToken(authResponse.access_token, DateTime.Now.AddSeconds(int.Parse(authResponse.expires_in)));
            Session session = new SessionBase(refreshToken, accessToken);

            return Result<Session>.Success(session);
        }

        #region DTOs
        /// <summary>
        /// Payload model from /api/auth/login, /api/auth/refresh
        /// </summary>
        [System.Serializable]
        internal class RegisterPayload
        {
            public string login;
            public string password;
        }

        /// <summary>
        /// Response model from /api/auth/login, /api/auth/refresh
        /// </summary>
        [System.Serializable]
        internal class RegisterResponse
        {
            public string status;
            public string message;
            public string access_token;
            public string expires_in;
        }

        #endregion
    
    }
}
