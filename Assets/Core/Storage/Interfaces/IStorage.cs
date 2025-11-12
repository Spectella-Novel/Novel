using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Storage.Interfaces
{

    public interface IStorage
    {
        public Task<Result> Save<T>(string tag, T value);
        public Task<Result<T>> Restore<T>(string tag);
    }
}
