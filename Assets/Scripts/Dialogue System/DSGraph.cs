using DialogueSystem.Data;
using DialogueSystem.Data.Utility;
using DialogueSystem.Enums;
using DialogueSystem.Nodes;
using DialogueSystem.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using XNode;
using Debug = UnityEngine.Debug;

namespace DialogueSystem
{
    [CreateAssetMenu]
    public class DSGraph : NodeGraph
    {
        public EntryPoint EntryPoint { get => entryPoint; private set => entryPoint = value; }

        private bool loop = false;

        [SerializeField] private EntryPoint entryPoint;

        public override Node AddNode(Type type)
        {
            if (type == typeof(EntryPoint) && EntryPoint != null)
                return null;

            var node = base.AddNode(type);

            EntryPoint = node as EntryPoint;

            return node;
        }


        private HashSet<TNode> StartToEndSteps<TNode, T>(TNode node, Action<TNode, TNode> action) where TNode : NodeBase<T>
        {

            HashSet<TNode> startNodes = GetStartNodes<TNode, T>(node, new HashSet<TNode>(), new HashSet<TNode>());
            if (startNodes.Count == 0) { 
                Debug.LogWarning("В нодах данных нет начальной точки!");
            }
            return Walker<TNode, T>(startNodes, action);

        }



        private HashSet<TNode> Walker<TNode, T>(HashSet<TNode> startNodes, Action<TNode, TNode> action) where TNode : NodeBase<T>
        {
            return Walker<TNode, T>(startNodes, new HashSet<TNode>(), action);
        }

        private HashSet<TNode> Walker<TNode, T>(HashSet<TNode> startNodes, HashSet<TNode> visited, Action<TNode, TNode> action) where TNode : NodeBase<T>
        {
            var waitingNodes = new Dictionary<TNode, int>();

            foreach (var node in startNodes)
            {
                visited.AddRange(Walker<TNode, T>(node, new HashSet<TNode>(), waitingNodes, action));
            }
            return visited;
        }

        private HashSet<TNode> Walker<TNode, T>(TNode node, HashSet<TNode> visited, Dictionary<TNode, int> waitingNodes, Action<TNode, TNode> action) where TNode : NodeBase<T>
        {
            if (visited.Contains(node)) return visited;

            visited.Add(node);

            foreach (var port in node.Outputs)
            {
                foreach (var connection in port.GetConnections())
                {
                    var nextNode = connection.node as TNode;
                    if (nextNode == null) continue;

                    action.Invoke(node, nextNode);
                }
 
            }

            if (node.Outputs == null) return visited;

            var outputs = node.Outputs?.ToList();

            foreach (var output in outputs)
            {
                foreach (var connectionPort in output.GetConnections())
                {
                    var dataNode = connectionPort.node as TNode;

                    if (dataNode == null) return visited;

                    visited = Walker<TNode, T>(dataNode, visited, waitingNodes, action);
                }
            }
            return visited;

        }


        private HashSet<TNode> GetStartNodes<TNode, T>(TNode node, HashSet<TNode> visited, HashSet<TNode> starts) where TNode : NodeBase<T>
        {
            if (visited.Contains(node)) return starts;

            visited.Add(node);
            if(node.InputsDataNode.Count == 0)
            {
                starts.Add(node);
                return starts;
            }

            var inputs = node.InputsDataNode.ToList();

            foreach (var input in inputs)
            {
                if (input == null) return starts;

                GetStartNodes<TNode, T>((TNode)input, visited, starts);
            }
            
            return starts;
        }

        internal void RefreshData<T>(NodeBase<T> dataNodeBase)
        {
            if (LoopProtection()) return;

            IDictionary<T, UnityUniversalWrapper> refreshData = new Dictionary<T, UnityUniversalWrapper>();
            dataNodeBase.Data.Clear();
            var visited =  StartToEndSteps<NodeBase<T>, T>(dataNodeBase, (node, nextNode) =>
            {
                refreshData = node.ModifyData(nextNode.Data);
                nextNode.UpdateData(refreshData);
            });

            EndLoopProtection();
        }

        private bool LoopProtection()
        {
            if (loop)
            {
                StackTrace stackTrace = new StackTrace();
                Debug.LogWarning($"Были обнаружены признаки петли \n {stackTrace}");
                return loop;
            }
            loop = true;
            return !loop;
        }

        private void EndLoopProtection()
        {
            loop = false;
        }


    }
}