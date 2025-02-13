using DialogueSystem.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace DialogueSystem.Nodes
{
    [NodeWidth(300)]
    public class DialogueNode : NodeBase<NovelTypes.Prefab>
    {
        [Input] public string Previous;

        [Output(dynamicPortList = true)] public List<string> Answers;

        [TextArea] public string Text;

        protected override bool IsValidConnection(NodePort from, NodePort to)
        {
            var isValid = true;

            var portPrevious = GetInputPort(nameof(Previous));
            if(portPrevious == to)
            {
                isValid = !portPrevious.GetConnections().Any(connection => connection.node is DataNode node && node != from.node);
            }

            return base.IsValidConnection(from, to) && isValid;
        }
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