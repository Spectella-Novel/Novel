using DialogueSystem.Data;
using XNode;
using DialogueSystem.Data.Utility;
using System.Collections.Generic;
using DialogueSystem.Enums;
using UnityEngine;
using System.Linq;
using DialogueSystem.Types;

namespace DialogueSystem.Nodes
{
    public abstract class NodeBase<T> : Node
    {
        // Используем универсальную обёртку вместо System.Object
        public SerializableDictionary<T, UniversalWrapper> Data = new SerializableDictionary<T, UniversalWrapper>();

        public List<NodeBase<T>> InputsDataNode = new List<NodeBase<T>>();

        protected DSGraph dSGraph { get; private set; }

        protected override void Init()
        {
            base.Init();
            dSGraph = graph as DSGraph;
        }

        /// <summary>
        /// Сохраняет объект в словаре по ключу mark.
        /// Если значение null – удаляет запись.
        /// </summary>
        public void Set<V>(T mark, V value)
        {
            if (value == null)
            {
                Remove(mark);
                return;
            }

            var wrapper = new UniversalWrapper();
            wrapper.SetValue(value);

            Data[mark] = wrapper;

            dSGraph.RefreshData(this);
        }

        /// <summary>
        /// Извлекает объект из словаря по ключу mark и приводит к типу V.
        /// </summary>
        public V Get<V>(T mark)
        {
            if (Data.TryGetValue(mark, out var wrapper))
            {
                return wrapper.GetValue<V>();
            }
            return default;
        }

        public void Remove(T mark)
        {
            Data.Remove(mark);
            OnChange();

        }

        public virtual IDictionary<T, UniversalWrapper> ModifyData(IDictionary<T, UniversalWrapper> data)
        {
            data.Clear();
            var modifyData = data.MergeDictionaries(Data);
            return modifyData;
        }

        public virtual void UpdateData(IDictionary<T, UniversalWrapper> data)
        {
            Data = new SerializableDictionary<T, UniversalWrapper>(data);
        }

        public virtual void OnChange()
        {
            dSGraph.RefreshData(this);
        }

        public override void OnRemoveConnection(NodePort port)
        {
            InputsDataNode.Clear();

            var inputNodes = Inputs
                .SelectMany(input => input.GetConnections())
                .Select(conn => conn.node as NodeBase<T>)
                .Where(node => node != null);

            InputsDataNode.AddRange(inputNodes);

            OnChange();
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            if (!IsValidConnection(from, to))
            {
                from.Disconnect(to);
                return;
            }

            var toNode = (NodeBase<T>)to.node;
            var fromNode = (NodeBase<T>)from.node;

            if (toNode == this)
            {
                InputsDataNode.Add(fromNode);
                OnChange();
                return;
            }
        }

        protected virtual bool IsValidConnection(NodePort from, NodePort to)
        {
            bool isValid = from.node is NodeBase<T> && to.node is NodeBase<T>;
            if (!isValid)
            {
                from.Disconnect(to);
            }
            return isValid;
        }
    }
}
