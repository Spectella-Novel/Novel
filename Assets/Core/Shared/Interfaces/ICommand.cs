using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared
{
    public interface ICommand
    {
        public Task<Result> Execute();
        public Task<Result> Undo();
    }
}
