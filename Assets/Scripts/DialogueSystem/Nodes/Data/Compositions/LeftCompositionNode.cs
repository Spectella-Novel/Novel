
using DialogueSystem.Enums;
using DialogueSystem.Types;
using System.Collections.Generic;

namespace DialogueSystem.Nodes.Data.Compositions
{

    public class LeftCompositionNode : CompositionBaseNode
    {
        public Character Left;
        public override IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> ModifyData(IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> data)
        {
            SetCharacter(Character.Position.HorizontalAlignment.Left, Left);
            return base.ModifyData(data);
        }
    }
}
