using DialogueSystem.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace DialogueSystem
{
    public static class Walker
    {
        public static bool DefaultCondition<TNode>(TNode Node) where TNode : NovelNode => true;
        public static HashSet<TNode> StartToEndSteps<TNode>(TNode node, Action<TNode, TNode> action) where TNode : NovelNode
        {
            HashSet<TNode> startNodes = GetStartNodes<TNode>(node, new HashSet<TNode>(), new HashSet<TNode>());
            if (startNodes.Count == 0)
            {
                Debug.LogWarning("В нодах данных нет начальной точки!");
            }
            return StepNext<TNode>(startNodes, action);

        }

        public static HashSet<TNode> StepNext<TNode>(
            TNode startNode, 
            Action<TNode, TNode> action, 
            Func<TNode, bool> callCondition = null, 
            Func<TNode, bool> endCondition = null, 
            int maxDepth = -1) where TNode : NovelNode
        {
            callCondition ??= DefaultCondition;
            endCondition ??= node => !DefaultCondition(node);
            
            return StepNext(
                new HashSet<TNode> { startNode }, 
                new HashSet<TNode>(), 
                action, 
                callCondition, 
                endCondition, 
                maxDepth);
        }

        public static HashSet<TNode> StepNext<TNode>(
            HashSet<TNode> startNodes, 
            Action<TNode, TNode> action, 
            Func<TNode, bool> callCondition = null, 
            Func<TNode, bool> endCondition = null, 
            int maxDepth = -1) where TNode : NovelNode
        {
            callCondition ??= DefaultCondition;
            endCondition ??= node => !DefaultCondition(node);

            return StepNext(startNodes, new HashSet<TNode>(), action, callCondition, endCondition, maxDepth);
        }

        public static HashSet<TNode> StepNext<TNode>(
            HashSet<TNode> startNodes, 
            HashSet<TNode> visited, 
            Action<TNode, TNode> action, 
            Func<TNode, bool> callCondition, 
            Func<TNode, bool> endCondition, 
            int maxDepth) where TNode : NovelNode
        {
            var waitingNodes = new Dictionary<TNode, int>();

            foreach (var node in startNodes)
            {
                visited = StepNext(node, visited, waitingNodes, action, callCondition, endCondition, maxDepth);
            }
            return visited;
        }

        private static HashSet<TNode> StepNext<TNode>(
            TNode node,
            HashSet<TNode> visited,
            Dictionary<TNode, int> waitingNodes,
            Action<TNode, TNode> action,
            Func<TNode, bool> callCondition,
            Func<TNode, bool> endCondition,
            int maxDepth,
            int currentDepth = 0) where TNode : NovelNode
        {
            // Если достигнута максимальная глубина, останавливаем обход
            if (maxDepth != -1 && currentDepth >= maxDepth)
            {
                return visited;
            }

            if (visited.Contains(node))
                return visited;

            visited.Add(node);

            foreach (var port in node.Outputs)
            {
                foreach (var connection in port.GetConnections())
                {
                    var nextNode = connection.node as TNode;
                    if (nextNode == null) continue;

                    if (callCondition.Invoke(node))
                    {
                        action.Invoke(node, nextNode);
                        currentDepth++;
                    }
                }
            }

            if (node.Outputs == null || endCondition.Invoke(node))
                return visited;

            var outputs = node.Outputs?.ToList();

            foreach (var output in outputs)
            {
                foreach (var connectionPort in output.GetConnections())
                {
                    var dataNode = connectionPort.node as TNode;
                    if (dataNode == null)
                        continue;

                    // Рекурсивно вызываем Step с увеличенной глубиной
                    visited = StepNext(dataNode, visited, waitingNodes, action, callCondition, endCondition, maxDepth, currentDepth);
                }
            }

            return visited;
        }


        public static HashSet<TNode> Step<TNode>(
            TNode startNode, 
            Action<TNode> action, 
            Func<TNode, bool> callCondition = null, 
            Func<TNode, bool> endCondition = null, 
            int maxDepth = -1) where TNode : NovelNode
        {
            callCondition ??= DefaultCondition;
            endCondition ??= node => !DefaultCondition(node);

            return Step(new HashSet<TNode> { startNode }, new HashSet<TNode>(), action, callCondition, endCondition, maxDepth);
        }

        public static HashSet<TNode> Step<TNode>(
            HashSet<TNode> startNodes, 
            Action<TNode> action, 
            Func<TNode, bool> callCondition = null, 
            Func<TNode, bool> endCondition = null, 
            int maxDepth = -1) where TNode : NovelNode
        {
            callCondition ??= DefaultCondition;
            endCondition ??= node => !DefaultCondition(node);

            return Step(startNodes, new HashSet<TNode>(), action, callCondition, endCondition, maxDepth);
        }

        public static HashSet<TNode> Step<TNode>(
            HashSet<TNode> startNodes, 
            HashSet<TNode> visited, 
            Action<TNode> action, 
            Func<TNode, bool> callCondition, 
            Func<TNode, bool> endCondition, 
            int maxDepth) where TNode : NovelNode
        {
            var waitingNodes = new Dictionary<TNode, int>();

            foreach (var node in startNodes)
            {
                visited = Step(node, visited, waitingNodes, action, callCondition, endCondition, maxDepth);
            }
            return visited;
        }

        private static HashSet<TNode> Step<TNode>(
            TNode node,
            HashSet<TNode> visited,
            Dictionary<TNode, int> waitingNodes,
            Action<TNode> action,
            Func<TNode, bool> callCondition,
            Func<TNode, bool> endCondition,
            int maxDepth = -1,
            int currentDepth = 0) where TNode : NovelNode
        {
            // Если достигнута максимальная глубина, останавливаем обход
            if (maxDepth != -1 && currentDepth >= maxDepth)
            {
                return visited;
            }

            if (visited.Contains(node))
                return visited;

            visited.Add(node);

            if (callCondition.Invoke(node))
            {
                action.Invoke(node);
                currentDepth++;
            }

            if (endCondition.Invoke(node)) 
                return visited;

            if (node.Outputs == null)
                return visited;

            var outputs = node.Outputs?.ToList();

            foreach (var output in outputs)
            {
                foreach (var connectionPort in output.GetConnections())
                {
                    var dataNode = connectionPort.node as TNode;
                    if (dataNode == null)
                        continue;
                    visited = Step(dataNode, visited, waitingNodes, action, callCondition, endCondition, maxDepth, currentDepth);
                }
            }

            return visited;
        }


        public static HashSet<TNode> GetStartNodes<TNode>(TNode node, HashSet<TNode> visited, HashSet<TNode> starts) where TNode : NovelNode
        {
            if (visited.Contains(node)) return starts;

            visited.Add(node);
            if (node.InputsDataNode.Count == 0)
            {
                starts.Add(node);
                return starts;
            }

            var inputs = node.InputsDataNode.ToList();

            foreach (var input in inputs)
            {
                if (input == null) return starts;

                GetStartNodes<TNode>((TNode)input, visited, starts);
            }

            return starts;
        }

    }
}
