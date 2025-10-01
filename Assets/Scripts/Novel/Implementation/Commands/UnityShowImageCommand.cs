using Novel.Loader;
using RenDisco;
using RenDisco.Commands;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Implementation.Commands
{
    internal class UnityShowImageCommand : ShowImageCommand
    {
        public UnityShowImageCommand(Show instruction, SynchronizationContext synchronizationContext) : base(instruction, synchronizationContext) { }
        public Image image { get; private set; }
        public override InstructionResult Execute()
        {

            InvokeInContext(image =>
            {
                var texture = FileLoader.LoadImage(Instruction.Image);

            }, image);
            return null;
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
