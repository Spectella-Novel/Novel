using Novel.Loader;
using Novel.Managers;
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
        public UnityDialogueCommand(Dialogue instruction, DialogueManager dialogueComponent, SynchronizationContext synchronizationContext) : base(instruction, synchronizationContext)
        {
            DialogueComponent = dialogueComponent;
        }

        public DialogueManager DialogueComponent { get; }

        public override ControlFlowSignal Flow()
        {
            InvokeInContext(dialoigue =>
            {
                dialoigue.SetDialogueText(Instruction.Text);
            }, DialogueComponent);
            Debug.Log("Ждум");
            Debug.Log(FileLoader.GetPath("", ""));
            SignalBroker.Emit(DefaultSignals.AnyAction);
            WaitableMessageBroker.WaitForMessage(DefaultSignals.AnyAction);
            return null;
        }

        public override void Undo(){}
    }
}
