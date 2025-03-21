
using DialogueSystem.Enums;
using DialogueSystem.Types;
using System.Collections.Generic;

namespace DialogueSystem.Nodes.Data.Compositions
{

    public class CentralCompositionNode : CompositionBaseNode
    {
        public Character Center;

        public override IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> ModifyData(IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> data)
        {
            SetCharacter(Character.Position.HorizontalAlignment.Center, Center);
            return base.ModifyData(data);
        }

    }
}
