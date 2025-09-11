using RenDisco;
using RenDisco.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    internal class UnityDefineCommand : DefineCommand
    {
        public UnityDefineCommand(Define instruction, IStorage storage, SynchronizationContext synchronizationContext) : base(instruction, storage, synchronizationContext)
        {
        }

        public override InstructionResult Execute() { 


            Storage.Set(Instruction.Name, Instruction.Value);
            return null;

        }

        public override void Undo()
        {
            
        }
    }
}
