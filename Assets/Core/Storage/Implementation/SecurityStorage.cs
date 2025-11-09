using Core.Shared;
using Core.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Storage.Implementation
{
    internal class SecurityStorage : IStorage
    {
        public Result<T> Restore<T>(string tag)
        {
            throw new NotImplementedException();
        }

        public Result Save<T>(string tag, T value)
        {
            throw new NotImplementedException();
        }
    }
}
