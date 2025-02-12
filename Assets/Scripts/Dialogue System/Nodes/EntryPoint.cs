using DialogueSystem.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.Nodes
{
    public class EntryPoint : NodeBase<NovelTypes.Prefab>
    {
        [Output(connectionType = ConnectionType.Override)] public int Start;
        public override IDictionary<NovelTypes.Prefab, Object> ModifyData(IDictionary<NovelTypes.Prefab, Object> data)
        {
            return data;
        }
    }
}