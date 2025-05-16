using DialogueSystem.Dictionary;
using DialogueSystem.Models.Enums;
using DialogueSystem.Types;
using System.Collections.Generic;


namespace DialogueSystem.Models.Dialogue
{
    public abstract class DialogueBase
    {
        public SerializableDictionary<NovelTypes.Prefab, UnityUniversalWrapper> Data;
        public string Text;
        public List<Answer> Answers;

        public DialogueBase()
        {
            Text = "Default";
            Data = new ();
            Answers = new();

        }
        public void Init(List<Answer> answers)
        {
            Answers = answers;
        }

        public struct Answer
        {
            public string Text;
            public DialogueBase Next;
        }
    }
}
