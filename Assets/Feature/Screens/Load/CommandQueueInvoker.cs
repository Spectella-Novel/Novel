using Core.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Novel.Feature.Screens.Load
{
    public class CommandQueueInvoker : ICommandQueueInvoker
    {
        private Queue<ICommand> _commands = new();
        private Stack<ICommand> _executedCommand = new Stack<ICommand>();

        public int Length => _commands.Count;
        public int ExecutedLength => _executedCommand.Count;

        public void Add(ICommand command)
        {
            _commands.Enqueue(command);
        }

        public async Task<Result> Invoke()
        {
            if (_commands.Count <= 0)
            {
                return Result.Failure("command is empty");
            }

            var command = _commands.Dequeue();

            var result = await command.Execute();

            if (!result.IsSuccess)
            {
                return result;
            }

            _executedCommand.Push(command);

            return Result.Success();
        }


        public async Task<Result> Undo()
        {
            if(_executedCommand.Count <= 0)
            {
                return Result.Failure("executedCommand is empty");
            }

            var command = _executedCommand.Pop();

            var result = await command.Undo();
            
            return result;
        }
    }
}

