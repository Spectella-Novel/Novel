using DialogueSystem.Enums;
using DialogueSystem.Nodes.Data;
using DialogueSystem.Types;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace DialogueSystem.Nodes
{
    [NodeWidth(300)]
    public abstract class BaseDialogueNode : NodeBase<NovelTypes.Prefab>
    {
        [Input] public string Previous;


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

    }
}