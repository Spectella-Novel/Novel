namespace Core.Auth.Interfaces
{
    public interface ICredentials
    {
        public void Set<T>(string key, T value);
        public T Get<T>(string key);
    }
}
