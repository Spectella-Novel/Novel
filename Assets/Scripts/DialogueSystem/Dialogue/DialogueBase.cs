using DialogueSystem.Data;
using DialogueSystem.Enums;
using DialogueSystem.Nodes;
using DialogueSystem.Nodes.Data;
using DialogueSystem.Nodes.Dialogue;
using DialogueSystem.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml;

namespace DialogueSystem.Dialogue
{
    public abstract class DialogueBase
    {
        public SerializableDictionary<NovelTypes.Prefab, UnityUniversalWrapper> Data;
        public string Text;
        public List<Answer> Answers;

        public DialogueBase(DialogueNodeBase node)
        {
            Data = node.Data;
            Text = node.Text;
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
