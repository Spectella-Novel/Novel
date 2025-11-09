using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NetworkRepositories.Interfaces
{
    public interface ICredentials
    {
        public void Set<T>(string key, T value);
        public T Get<T>(string key);
    }
}
