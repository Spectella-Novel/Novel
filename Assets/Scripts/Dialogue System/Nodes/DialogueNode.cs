using DialogueSystem.Enums;
using System.Collections.Generic;
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

        [Input(connectionType = ConnectionType.Override)] public DataNode DataNode;

        public override void UpdateData(IDictionary<NovelTypes.Prefab, Object> data)
        {
            base.UpdateData(data);
        }
        protected override bool IsValidConnection(NodePort from, NodePort to)
        {
            var isValid = to != GetInputPort(nameof(Previous)) || from.node is DialogueNode || from.node is EntryPoint;

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