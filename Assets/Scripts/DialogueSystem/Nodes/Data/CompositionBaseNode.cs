using DialogueSystem.Characters;
using DialogueSystem.Enums;
using DialogueSystem.Types;
using System.Collections.Generic;

namespace DialogueSystem.Nodes.Data
{
    public class CompositionBaseNode : DataNodeBase
    {
        public Character Main;

        private Dictionary<Character.Position.HorizontalAlignment, Character> _compositionList = new();

        public override IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> ModifyData(IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> data)
        {
            var scene = new Composition(Main);

            foreach (var pair in _compositionList)
            {
                scene.Set(new Character.RelativePosition(pair.Key), pair.Value);
            }

            data[NovelTypes.Prefab.Characters] = new UnityUniversalWrapper(scene);

            return base.ModifyData(data);
        }
        public void SetCharacter(Character.Position.HorizontalAlignment alignment, Character character)
        {
            _compositionList[alignment] = character;
        }
    }
}
