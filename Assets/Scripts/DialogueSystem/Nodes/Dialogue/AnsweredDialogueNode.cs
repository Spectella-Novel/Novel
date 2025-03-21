using System.Collections.Generic;
using XNode;

namespace DialogueSystem.Nodes.Dialogue
{
    [NodeWidth(300)]
    public class AnsweredDialogueNode : DialogueNodeBase
    {
        [Output(dynamicPortList = true)] public List<string> Answers;

        public override List<NodePort> GetNext()
        {
            var answers = new List<NodePort>();

            KeyValuePair<string, DialogueNodeBase> answer;

            for (int i = 0; i < this.Answers.Count; i++)
            {
                string text = this.Answers[i];

                var port = this.GetOutputPort($"{nameof(this.Answers)} {i}");

                answers.Add(port);
            }
            return answers;
        }

        public override DialogueNodeBase GetNextDialogue(int id = 0)
        {
            return GetNextDialogue($"{nameof(Answers)} {id}");
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName.Contains(nameof(Answers)))
            {
                return this;
            }

            return base.GetValue(port);
        }
    }
}