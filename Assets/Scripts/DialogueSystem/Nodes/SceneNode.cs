using DialogueSystem.Enums;
using DialogueSystem.Nodes.Data;
using UnityEngine;
using XNode;

namespace DialogueSystem.Nodes
{
    public class SceneNode : Node
    {

        [Input(connectionType = ConnectionType.Override)] public Texture2D Background;

        public AudioClip Music;

        [Output] public SceneNode Output;

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            if (to.fieldName == nameof(Background) &&
                from.node is DataNode prefabNode &&
                prefabNode.PrefabType == NovelTypes.Prefab.Sprite)
            {
                Background = prefabNode.Get<Texture2D>(prefabNode.PrefabType);
            }

            base.OnCreateConnection(from, to);
        }
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(Output))
                return this;
            return base.GetValue(port);
        }
    }
}