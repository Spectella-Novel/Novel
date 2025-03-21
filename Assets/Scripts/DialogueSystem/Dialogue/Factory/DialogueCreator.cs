using DialogueSystem.Nodes.Data;
using DialogueSystem.Nodes.Dialogue;
using DialogueSystem.Nodes;
using System.Collections.Generic;
using System.Linq;

using static DialogueSystem.Walker;
using UnityEditor.PackageManager.Requests;

namespace DialogueSystem.Dialogue.Factory
{
    public class DialogueCreator
    {
        //DialogueFactory factory;
        
        //public DialogueCreator() 
        //{ 
        //    factory = new DialogueFactory();
        //}
        
        //public DialogueBase Create(DialogueNodeBase Start)
        //{

        //    Step(Start, (node) =>
        //    {
        //        var nextNodes = node.GetNext();

        //        var pairs = nextNodes.Where(pair => pair.Value.node is not DialogueNodeBase);

        //        foreach (var pair in pairs)
        //        {
        //            var dataNode = pair.Value.node as NovelNode;

        //            if (dataNode == null) continue;

        //            Step(dataNode, dialogueNode =>
        //            {
        //                dataNode = dialogueNode;
        //            },
        //            dsNode => dsNode is DialogueNodeBase,
        //            maxDepth: 1);
        //            var dialogueNode = dataNode as DialogueNodeBase;
        //            pair.Value.ClearConnections();
        //            pair.Value.AddConnections(dialogueNode.GetInputPort(nameof(dialogueNode.Previous)));

        //        }
        //    },
        //    node => node is DialogueNodeBase);
        //}

        //public static bool TryGetDialogue(NovelNode dialogueNode, out DialogueNodeBase NextDialogues)
        //{
        //    NextDialogues = GetDialogueNode(dialogueNode);
        //    return true;
        //}

        //private static DialogueNodeBase GetDialogueNode(NovelNode node)
        //{
        //    IEnumerable<NovelNode> outputs;
        //    DialogueNodeBase dialogueNode = default;
        //    NovelNode nextNode = node;

        //    do {
        //        outputs = nextNode?.Outputs
        //                    .SelectMany(port => port.GetConnections())
        //                    .Select(port => port.node)
        //                    .OfType<NovelNode>();

        //        dialogueNode = outputs?.OfType<DialogueNodeBase>().FirstOrDefault();

        //        nextNode = outputs.FirstOrDefault();

        //    } while (dialogueNode != null && outputs.Count() != 0 );

        //    return dialogueNode;
        
        //}
    }
}
