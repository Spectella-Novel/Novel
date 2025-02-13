using DialogueSystem.Enums;
using DialogueSystem.Types;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.Nodes
{
    public class EntryPoint : NodeBase<NovelTypes.Prefab>
    {
        [Output(connectionType = ConnectionType.Override)] public int Start;
        public override IDictionary<NovelTypes.Prefab, UniversalWrapper> ModifyData(IDictionary<NovelTypes.Prefab, UniversalWrapper> data)
        {
            return data;
        }
    }
}