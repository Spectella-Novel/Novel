using DialogueSystem.Enums;
using DialogueSystem.Nodes;
using DialogueSystem.Nodes.Dialogue;
using DialogueSystem.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using XNode;
using Debug = UnityEngine.Debug;

using static DialogueSystem.Walker;

namespace DialogueSystem
{
    [CreateAssetMenu]
    public class DialogueGraph : NodeGraph
    {
        public EntryPoint EntryPoint { get => entryPoint; private set => entryPoint = value; }

        public List<Character> CharacterList = new List<Character>();

        private bool loop = false;

        [SerializeField] private EntryPoint entryPoint;
        public void Init()
        {
            RefreshData();
            var start = entryPoint.GetPort(nameof(entryPoint.Start))?.Connection?.node as DialogueNodeBase;

            //Step(start, (node) =>
            //{
            //    var nextNodes = node.GetNext();

            //    var nodePorts = nextNodes.Where(nodePort => nodePort?.Connection?.node is not DialogueNodeBase).ToList();

            //    foreach (var nodePort in nodePorts)
            //    {
            //        var dataNode = nodePort.Connection?.node as NovelNode;

            //        if (dataNode == null) continue;

            //        Step(dataNode, dialogueNode =>
            //        {
            //            dataNode = dialogueNode;
            //        },
            //        dsNode => dsNode is DialogueNodeBase,
            //        maxDepth: 1);
            //        var dialogueNode = dataNode as DialogueNodeBase;
            //        nodePort.ClearConnections();
            //        nodePort.Connect(dialogueNode.GetInputPort(nameof(dialogueNode.Previous)));
                        
            //    }
            //},
            //node => node is DialogueNodeBase);
        }

        public override Node AddNode(Type type)
        {
            if(!type.IsSubclassOf(typeof(NovelNode))) return null;

            var node = base.AddNode(type);

            if (type != typeof(EntryPoint)) return node;

            if (EntryPoint == null) EntryPoint = node as EntryPoint;
            
            return EntryPoint;
        }

        internal void RefreshData()
        {
            if (LoopProtection()) return;

                var endNodes = new HashSet<NovelNode>();
                var nodeBases = nodes.Cast<NovelNode>().Where(x => x != null).ToList();

                foreach (var node in nodeBases)
                {
                    node.Data.Clear();
                    var nextNodes = node.Inputs.Select(x => x.GetConnections().Count()).Sum();
                    if(nextNodes == 0)
                    {
                        endNodes.Add(node);
                    }
                }


                foreach (var node in endNodes)
                {
                    IDictionary<NovelTypes.Prefab, UnityUniversalWrapper> refreshData = new Dictionary<NovelTypes.Prefab, UnityUniversalWrapper>();
                    var visited = StartToEndSteps<NovelNode>(node, (node, nextNode) =>
                    {
                        refreshData = node.ModifyData(nextNode.Data);
                        nextNode.UpdateData(refreshData);
                    });

                }

            EndLoopProtection();
        }

        private bool LoopProtection()
        {
            if (loop)
            {
                StackTrace stackTrace = new StackTrace();
                Debug.LogWarning($"Были обнаружены признаки петли \n {stackTrace}");
                EndLoopProtection();
                return true;
            }
            loop = true;
            return false;
        }

        private void EndLoopProtection()
        {
            loop = false;
        }
    }
}