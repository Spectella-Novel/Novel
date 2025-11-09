using System.Collections.Generic;

namespace Core.NetworkRepositories.Implementation
{

    public class Credentials
    {
        private readonly Dictionary<string, object> _data = new();

        public void Set<T>(string key, T value)
        {
            _data[key] = value;
        }

        public T Get<T>(string key)
        {
            return (T)_data[key];
        }
    }
    public class LoginCredentials : Credentials
    {
        public readonly string Login;
        public readonly string Password;

        public LoginCredentials(string login, string password)
        {
            Login = login;
            Password = password;
            Set(nameof(Login), Login);
            Set(nameof(Password), Password);
        }
    }
}
