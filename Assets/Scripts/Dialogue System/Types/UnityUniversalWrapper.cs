using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace DialogueSystem.Types
{
    public enum UnityWrapperType
    {
        None,
        UnityObject, // Для объектов, наследуемых от UnityEngine.Object
        Any       // Для произвольных ссылочных объектов
    }

    [Serializable]
    public class UnityUniversalWrapper : ISerializationCallbackReceiver
    {
        public UnityWrapperType wrapperType = UnityWrapperType.None;

        // Для UnityEngine.Object – сериализуется стандартно.
        [SerializeField]
        public UnityEngine.Object unityObjectValue;

        // Для произвольных ссылочных объектов.
        [SerializeField]
        public object collectionValue;

        [SerializeField]
        public string serializedData;

        // Поле для хранения типа объекта (не сериализуется напрямую)
        [NonSerialized]
        public Type serializedType;

        // Временное поле для сериализации имени типа
        [SerializeField]
        private string serializedTypeName;

        public UnityUniversalWrapper() { }

        public UnityUniversalWrapper(object value)
        {
            SetValue(value);
        }

        public void SetValue(object value)
        {
            switch (value)
            {
                case null:
                    wrapperType = UnityWrapperType.None;
                    unityObjectValue = null;
                    collectionValue = null;
                    break;

                case UnityEngine.Object obj:
                    wrapperType = UnityWrapperType.UnityObject;
                    unityObjectValue = obj;
                    collectionValue = null;
                    break;

                default:
                    wrapperType = UnityWrapperType.Any;
                    collectionValue = value;
                    unityObjectValue = null;
                    break;
            }
        }

        public T GetValue<T>()
        {
            if (wrapperType == UnityWrapperType.UnityObject)
            {
                return (T)(object)unityObjectValue;
            }
            else if (wrapperType == UnityWrapperType.Any)
            {
                if (collectionValue is T value)
                {
                    return value;
                }
            }
            return default;
        }

        public override string ToString()
        {
            switch (wrapperType)
            {
                case UnityWrapperType.UnityObject:
                    return unityObjectValue ? unityObjectValue.name : "null";
                case UnityWrapperType.Any:
                    return collectionValue != null ? collectionValue.ToString() : "null";
                default:
                    return "null";
            }
        }

        public void OnBeforeSerialize()
        {
            // Если значение коллекции не null, сохраняем его тип в виде строки и сериализуем объект в XML.
            if (collectionValue != null)
            {
                serializedType = collectionValue.GetType();
                serializedTypeName = serializedType.AssemblyQualifiedName;

                var serializer = new XmlSerializer(serializedType);
                using (var writer = new StringWriter())
                {
                    serializer.Serialize(writer, collectionValue);
                    serializedData = writer.ToString();
                }
            }
        }

        public void OnAfterDeserialize()
        {
            // Восстанавливаем тип из сохранённой строки.
            if (!string.IsNullOrEmpty(serializedTypeName))
            {
                serializedType = Type.GetType(serializedTypeName);
            }

            if (!string.IsNullOrEmpty(serializedData))
            {
                try
                {
                    // Используем восстановленный тип для десериализации.
                    Type type = serializedType;
                    if (type == null) return;

                    var serializer = new XmlSerializer(type);
                    using (var reader = new StringReader(serializedData))
                    {
                        collectionValue = serializer.Deserialize(reader);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to deserialize object: " + e.Message);
                }
            }
        }
    }
}
