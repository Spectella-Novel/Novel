using DialogueSystem.Enums;
using DialogueSystem.Nodes.Data;
using DialogueSystem.Types;
using System.Collections.Generic;
using XNode;

namespace DialogueSystem.Nodes
{
    [NodeWidth(300)]
    public class AnsweredDialogueNode : BaseDialogueNode
    {
        [Output(dynamicPortList = true)] public List<string> Answers;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName.Contains(nameof(Answers)))
            {
                return this;
            }

            return base.GetValue(port);
        }
    }
}