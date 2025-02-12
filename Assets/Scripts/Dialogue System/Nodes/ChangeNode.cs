using DialogueSystem.Enums;
using System.Collections.Generic;

namespace DialogueSystem.Nodes
{
    //Унаследовать Change и Data от одного класса
    class ChangeNode : DataNodeBase<NovelTypes.Prefab>
    { 

        public override IDictionary<NovelTypes.Prefab, UnityEngine.Object> ModifyData(IDictionary<NovelTypes.Prefab, UnityEngine.Object> data)
        {

            data = base.ModifyData(data);
            data[PrefabType] = Prefab;

            return data;
        }
    }
}