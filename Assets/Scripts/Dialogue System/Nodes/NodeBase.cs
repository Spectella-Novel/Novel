using DialogueSystem.Data;
using XNode;
using DialogueSystem.Data.Utility;
using System.Collections.Generic;
using DialogueSystem.Enums;
using UnityEngine;
using System.Linq;

namespace DialogueSystem.Nodes
{
    public abstract class NodeBase<T> : Node
    {
        // Инициализация словаря для избежания NullReferenceException.
        public SerializableDictionary<T, System.Object> Data = new();
        
        public List<NodeBase<T>> InputsDataNode = new List<NodeBase<T>>();

        protected DSGraph dSGraph { get; private set; }
        

        protected override void Init()
        {
            base.Init();
            dSGraph = graph as DSGraph;
        }
        /// <summary>
        /// Сохраняет объект в словаре, используя ключ, составленный из runtime-типа объекта и метки.
        /// </summary>
        public void Set(T mark, System.Object value)
        {
            if (value == null)
            {
                this.Remove(mark);
                return;
            }


            Data[mark] = value;

            dSGraph.RefreshData(this);
        }

        /// <summary>
        /// Извлекает объект из словаря по ключу, составленному из типа T и метки.
        /// </summary>
        public System.Object Get(T mark) 
        {
            if (Data.TryGetValue(mark, out var value))
            {
                return value;
            }
            return null;
        }

        public void Remove(T novelPrefabType)
        {
            if (Data.Remove(novelPrefabType))
            {
                OnChange();
            }
        }

        public virtual IDictionary<T, System.Object> ModifyData(IDictionary<T, System.Object> data)
        {
            data.Clear();
            var modifyData = data.MergeDictionaries(Data);
            return modifyData;
        }

        public virtual void UpdateData(IDictionary<T, System.Object> data)
        {
            Data = new(data);
        }

        public virtual void OnChange()
        {
            dSGraph.RefreshData(this);
        }

        
        public override void OnRemoveConnection(NodePort port)
        {

            if(port.node is NodeBase<T> node)
            {
                InputsDataNode.Clear();

                var inputNode = Inputs
                    .SelectMany(input => input.GetConnections())
                    .Select(port => port.node as NodeBase<T>)
                    .Where(node => node != null);

                InputsDataNode.AddRange(inputNode);
            }
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


            if (toNode == this) {
                InputsDataNode.Add(fromNode);
                OnChange();
                return; 
            }
        }

        protected virtual bool IsValidConnection(NodePort from, NodePort to)
        {
            var isValidConnection = from.node is NodeBase<NovelTypes.Prefab> && to.node is NodeBase<NovelTypes.Prefab>;
            if (!isValidConnection)
            {
                from.Disconnect(to);
            }
            return isValidConnection;
        }

    }
}
