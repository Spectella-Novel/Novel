using DialogueSystem.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.Nodes
{

    public class DataNode : DataNodeBase
    {
        [SerializeField] public NovelTypes.Prefab PrefabType;

        // Прямая ссылка на префаб (не сериализуется корректно через JSON)
        [SerializeField] public UnityEngine.Object Prefab;

        public override IDictionary<NovelTypes.Prefab, System.Object> ModifyData(IDictionary<NovelTypes.Prefab, System.Object> data)
        {
            data = base.ModifyData(data);
            if(Prefab != null) data[PrefabType] = Prefab;
            return data;
        }
     }
}