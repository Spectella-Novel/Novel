using DialogueSystem.Models.Dialogue;
using DialogueSystem.Models.Enums;
using DialogueSystem.Nodes.Data;
using DialogueSystem.Reactive;
using DialogueSystem.Reactive.Editors;
using DialogueSystem.Types;
using DialogueSystem.Types.Reactive;
using DialogueSystem.Types.Reactive.Editors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;
using static DialogueSystem.Dialogue.DialogueBase;
using static DialogueSystem.Walker;

namespace DialogueSystem.Nodes.Dialogue
{
    [NodeWidth(300)]
    public abstract class DialogueNodeBase : NovelNode
    {
        [Input] public string Previous;
        [ReactiveProperty] public ReactiveProperty<string> Text = new();

        public virtual void InitReactiveProperty()
        {
            Text.OnChange += value =>
            {
                Debug.Log(value);
                Model.Text = value;
            };
        }
        public abstract DialogueBase Model { get; protected set; }

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

        public override void UpdateData(IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> data)
        {
            Model.Data = new(data);
            base.UpdateData(data);
        }
    }
}