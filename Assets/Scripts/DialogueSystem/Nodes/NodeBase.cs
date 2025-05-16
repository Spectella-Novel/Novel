using DialogueSystem.Dictionary;
using XNode;
using DialogueSystem.Dictionary.Utility;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem.Types;

namespace DialogueSystem.Nodes
{
    public abstract class NodeBase<T> : Node
    {
        public SerializableDictionary<T, UnityUniversalWrapper> Data = new SerializableDictionary<T, UnityUniversalWrapper>();

        public List<NodeBase<T>> InputsDataNode = new List<NodeBase<T>>();

        protected DialogueGraph dSGraph { get; private set; }

        protected override void Init()
        {
            base.Init();
            dSGraph = graph as DialogueGraph;
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

            var wrapper = new UnityUniversalWrapper();
            wrapper.SetValue(value);

            Data[mark] = wrapper;

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
        }

        public virtual IDictionary<T, UnityUniversalWrapper> ModifyData(IDictionary<T, UnityUniversalWrapper> data)
        {
            var modifyData = Data.MergeDictionaries(data);
            return modifyData;
        }

        public virtual void UpdateData(IDictionary<T, UnityUniversalWrapper> data)
        {
            Data = new SerializableDictionary<T, UnityUniversalWrapper>(data);
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
                RefreshInputConnection();
                return;
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            RefreshInputConnection();
        }

        private void RefreshInputConnection()
        {
            InputsDataNode.Clear();

            var inputNodes = Inputs
                .SelectMany(input => input.GetConnections())
                .Select(conn => conn.node as NodeBase<T>)
                .Where(node => node != null);

            InputsDataNode.AddRange(inputNodes);
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
