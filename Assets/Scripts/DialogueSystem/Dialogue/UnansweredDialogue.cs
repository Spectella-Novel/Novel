using DialogueSystem.Enums;
using DialogueSystem.Nodes;
using DialogueSystem.Nodes.Data;
using DialogueSystem.Nodes.Dialogue;
using System.Linq;
using System.Xml;

namespace DialogueSystem.Dialogue
{
    public class UnansweredDialogue : DialogueBase
    {
        public UnansweredDialogue(UnansweredDialogueNode node) : base(node)
        {
        }
    }
}
