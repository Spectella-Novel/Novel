using UnityEditor;
using UnityEngine;

namespace DialogueSystem.Types.Reactive.Editors
{
    public class ReactivePropertyAttribute : PropertyAttribute { }
    [CustomPropertyDrawer(typeof(ReactivePropertyAttribute))]
    public class ReactivePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var valueProp = property.FindPropertyRelative("_value");
            EditorGUI.PropertyField(position, valueProp, label);
            CallValueSetter(property);

            EditorGUI.EndProperty();
        }

        private void CallValueSetter(SerializedProperty property)
        {
            var targetObject = property.serializedObject.targetObject;
            var targetObjectClassType = targetObject.GetType();
            var reactiveFieldInfo = targetObjectClassType.GetField(property.propertyPath);
            var reactiveType = reactiveFieldInfo.FieldType;

            // Получаем поле "_value" и свойство "Value"
            
            var field = reactiveType.GetField("_value", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var prop = reactiveType.GetProperty("Value", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            if (field != null && prop != null)
            {
                var reactiveField = reactiveFieldInfo.GetValue(targetObject);
                object newValue = field.GetValue(reactiveField);
                prop.GetSetMethod().Invoke(reactiveField, new object[] { newValue });
            }
        }
    }
}
