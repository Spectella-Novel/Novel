using RenDisco;
using RenDisco.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class UnityPlayMusicCommand : PlayMusicCommand
    {
        public UnityPlayMusicCommand(PlayMusic instruction, SynchronizationContext synchronizationContext) : base(instruction, synchronizationContext)
        {
        }

        public override ControlFlowSignal Flow()
        {
            Console.WriteLine(Instruction.FadeIn== default ? $"Play Music: {Instruction.File}" : $"Play Music: {Instruction.File} with fadein of {Instruction.FadeIn} second(s)");
            return null;
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
