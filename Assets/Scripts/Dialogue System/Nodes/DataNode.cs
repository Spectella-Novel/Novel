using DialogueSystem.Enums;
using System.Collections.Generic;

namespace DialogueSystem.Nodes
{

    public class DataNode : DataNodeBase<NovelTypes.Prefab>
    {
        public override IDictionary<NovelTypes.Prefab, UnityEngine.Object> ModifyData(IDictionary<NovelTypes.Prefab, UnityEngine.Object> data)
        {
            data.Clear();
            data = base.ModifyData(data);
            if(Prefab != null) data[PrefabType] = Prefab;
            return data;
        }
     }
}