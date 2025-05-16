using DialogueSystem.Models.Dialogue;
using DialogueSystem.Models.Enums;
using DialogueSystem.Types;
using System.Collections.Generic;
using XNode;
namespace DialogueSystem.Nodes.Dialogue
{
    public class UnansweredDialogueNode : DialogueNodeBase
    {
        [Output] public string Next;
        private DialogueBase model;

        public UnansweredDialogueNode()
        {
            Model = new UnansweredDialogue();
        }

        public override DialogueBase Model { get => model; protected set => model = value; }

        public override List<NodePort> GetNext()
        {
            return new List<NodePort>() {GetPort(nameof(Next))};
        }

        public override DialogueNodeBase GetNextDialogue(int id = 0)
        {
            return GetNextDialogue(nameof(Next));
        }

        public override void UpdateData(IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> data)
        {
            base.UpdateData(data);
        }
    }
}
