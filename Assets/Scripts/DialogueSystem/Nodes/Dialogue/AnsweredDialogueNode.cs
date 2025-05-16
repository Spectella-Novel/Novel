using DialogueSystem.Models.Dialogue;
using DialogueSystem.Types.Reactive;
using System.Collections.Generic;
using XNode;

namespace DialogueSystem.Nodes.Dialogue
{
    [NodeWidth(300)]
    public class AnsweredDialogueNode : DialogueNodeBase
    {
        [Output(dynamicPortList = true)] public ReactiveProperty<List<string>> Answers;

        public override DialogueBase Model { get => _model; protected set => _model = value; }
        private DialogueBase _model;

        public override void InitReactiveProperty()
        {
            base.InitReactiveProperty();


        }
        public override List<NodePort> GetNext()
        {
            var answers = new List<NodePort>();

            KeyValuePair<string, DialogueNodeBase> answer;

            for (int i = 0; i < this.Answers.Value.Count; i++)
            {
                string text = this.Answers.Value[i];

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