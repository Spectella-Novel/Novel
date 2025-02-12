using UnityEditor;
using UnityEngine;

namespace DialogueSystem.Nodes
{
    public abstract class DataNodeBase<T> : NodeBase<T>
    {
        [SerializeField] public T PrefabType;

        // Прямая ссылка на префаб (не сериализуется корректно через JSON)
        [SerializeField] public UnityEngine.Object Prefab;

        // Сериализуем GUID префаба
        [SerializeField] private string prefabGUID;

        [Output] public GameObject Output;
        [Input] public GameObject Input;
    }
}
