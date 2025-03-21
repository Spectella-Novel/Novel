using System.Collections.Generic;
using XNode;
namespace DialogueSystem.Nodes.Dialogue
{
    public class UnansweredDialogueNode : DialogueNodeBase
    {
        [Output] public string Next;

        public override List<NodePort> GetNext()
        {
            return new List<NodePort>() {GetPort(nameof(Next))};
        }

        public override DialogueNodeBase GetNextDialogue(int id = 0)
        {
            return GetNextDialogue(nameof(Next));
        }
    }
}
