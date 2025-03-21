using DialogueSystem.Enums;
using DialogueSystem.Nodes.Data;
using DialogueSystem.Types;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;
using static DialogueSystem.Walker;

namespace DialogueSystem.Nodes.Dialogue
{
    [NodeWidth(300)]
    public abstract class DialogueNodeBase : NovelNode
    {
        [Input] public string Previous;
        [TextArea] public string Text;

        protected override bool IsValidConnection(NodePort from, NodePort to)
        {
            var isValid = true;

            var portPrevious = GetInputPort(nameof(Previous));
            if(portPrevious == to && from.node is DataNode)
            {
                isValid = !portPrevious.GetConnections().Any(connection => connection.node is DataNode node && node != from.node);
            }

            return base.IsValidConnection(from, to) && isValid;
        }

        public abstract List<NodePort> GetNext();

        public abstract DialogueNodeBase GetNextDialogue(int id = 0);

        protected virtual DialogueNodeBase GetNextDialogue(string portName)
        {
            var nextPort = GetOutputPort(portName);

            var nextDataNode = nextPort.Connection?.node as NovelNode;

            if (nextDataNode == null)
                return null;

            DialogueNodeBase dataNode = nextDataNode as DialogueNodeBase;

            IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> refreshData;

            StepNext(nextDataNode, (node, nextNode) =>
            {
                if (nextNode is DialogueNodeBase dialogueNode) dataNode = dialogueNode;

                refreshData = node.ModifyData(nextNode.Data);
                nextNode.UpdateData(refreshData);
            },
            endCondition: node => node is DialogueNodeBase);

            return dataNode;
        }

    }
}