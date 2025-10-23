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
    internal class UnityNarrationCommand : NarrationCommand
    {
        public UnityNarrationCommand(Narration instruction, SynchronizationContext synchronizationContext) : base(instruction, synchronizationContext)
        {
        }

        public override ControlFlowSignal Flow()
        {
            Console.WriteLine($"Narration: {Instruction.Text}");
            return null;
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
