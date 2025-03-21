using DialogueSystem.Nodes.Dialogue;


namespace DialogueSystem.Dialogue
{
    public class DialogueFactory
    {
        public DialogueBase Create(DialogueNodeBase node)
        {
            return node switch
            {
                UnansweredDialogueNode unanswered => new UnansweredDialogue(unanswered),
                AnsweredDialogueNode answered => new AnsweredDialogue(answered),
            };
        }

    }
}
