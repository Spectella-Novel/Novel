using DialogueSystem.Enums;
using DialogueSystem.Types;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.Nodes.Data
{

    public class DataNode : DataNodeBase
    {
        [SerializeField, HideInInspector] public NovelTypes.Prefab PrefabType;

        [SerializeReference, HideInInspector] public UnityEngine.Object Prefab;

        public override IDictionary<NovelTypes.Prefab, UniversalWrapper> ModifyData(IDictionary<NovelTypes.Prefab, UniversalWrapper> data)
        {
            data = base.ModifyData(data);
            var value = new UniversalWrapper(Prefab);
            if (Prefab != null) data[PrefabType] = value;
            return data;
        }
    }
}