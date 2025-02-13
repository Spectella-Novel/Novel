using System;
using UnityEngine;

namespace DialogueSystem.Types
{
    public enum UniversalWrapperType
    {
        None,
        UnityObject, // Для объектов, наследуемых от UnityEngine.Object
        Reference    // Для произвольных ссылочных объектов
    }

    [Serializable]
    public class UniversalWrapper
    {
        public UniversalWrapperType wrapperType = UniversalWrapperType.None;

        // Для UnityEngine.Object – сериализуется стандартно.
        [SerializeField]
        public UnityEngine.Object unityObjectValue;

        // Для произвольных ссылочных объектов (используем [SerializeReference] для полиморфной сериализации).
        [SerializeReference]
        public object referenceValue;

        public UniversalWrapper() { }

        public UniversalWrapper(object value)
        {
            SetValue(value);
        }

        public void SetValue(object value)
        {
            if (value == null)
            {
                wrapperType = UniversalWrapperType.None;
                unityObjectValue = null;
                referenceValue = null;
            }
            else if (value is UnityEngine.Object)
            {
                wrapperType = UniversalWrapperType.UnityObject;
                unityObjectValue = value as UnityEngine.Object;
                referenceValue = null;
            }
            else
            {
                wrapperType = UniversalWrapperType.Reference;
                referenceValue = value;
                unityObjectValue = null;
            }
        }

        public T GetValue<T>()
        {
            if (wrapperType == UniversalWrapperType.UnityObject)
            {
                return (T)(object)unityObjectValue;
            }
            else if (wrapperType == UniversalWrapperType.Reference)
            {
                return (T)referenceValue;
            }
            return default;
        }

        public override string ToString()
        {
            switch (wrapperType)
            {
                case UniversalWrapperType.UnityObject:
                    return unityObjectValue ? unityObjectValue.name : "null";
                case UniversalWrapperType.Reference:
                    return referenceValue != null ? referenceValue.ToString() : "null";
                default:
                    return "null";
            }
        }
    }

}
