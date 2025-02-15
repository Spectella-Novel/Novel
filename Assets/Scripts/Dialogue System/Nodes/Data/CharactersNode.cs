using DialogueSystem.Enums;
using DialogueSystem.Nodes.Data;
using DialogueSystem.Types;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.Nodes.Data
{
    internal class CharactersNode : DataNodeBase
    {
        [SerializeField] public List<string> Characters;

        public override IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> ModifyData(IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> data)
        {
            data = base.ModifyData(data);
            var value = new UnityUniversalWrapper(Characters);
            if (Characters.Count != 0) data[NovelTypes.Prefab.Characters] = value;
            return data;
        }
    }
}
