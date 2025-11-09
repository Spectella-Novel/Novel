using Core.Shared;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Shared
{
    public interface ICommandInvoker
    {
        public void Add(ICommand command);

        public Task<Result> Invoke();
        public Task<Result> Undo();
    }
    public interface ICommandQueueInvoker : ICommandInvoker
    {
        public int Length { get; }
        public int ExecutedLength { get; }
    }
}

