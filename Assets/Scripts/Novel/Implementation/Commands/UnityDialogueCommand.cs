using RenDisco;
using RenDisco.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Implementation.Commands
{
    internal class UnityDialogueCommand : DialogueCommand
    {
        public UnityDialogueCommand(Dialogue instruction, DialogueComponent dialogueComponent, SynchronizationContext synchronizationContext) : base(instruction, synchronizationContext)
        {
            DialogueComponent = dialogueComponent;
        }

        public DialogueComponent DialogueComponent { get; }

        public override InstructionResult Execute()
        {
            
            InvokeInContext(dialoigue =>
            {
                Debug.Log("log");
            }, DialogueComponent);
            SynchronizationContext.Post(state => { Console.WriteLine(Thread.CurrentThread.Name + " Dialogue"); }, null);
            return null;
        }

        public override void Undo(){}
    }
}
