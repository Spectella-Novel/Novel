#if UNITY_EDITOR
using DialogueSystem.Enums;
using DialogueSystem.Nodes.Data;
using UnityEditor;
using UnityEngine;
using XNodeEditor;
#endif

#if UNITY_EDITOR
namespace DialogueSystem.Nodes.Editors
{
    [CustomNodeEditor(typeof(CharactersNode))]
    internal class CharactersNodeEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            // Получаем ссылку на текущий узел
            var node = target as CharactersNode;

            if (node == null) return;

            // Отображаем стандартные поля узла
            base.OnBodyGUI();
            if(node.Characters == null) return;
            // Отображаем список элементов
            EditorGUILayout.LabelField("Elements", EditorStyles.boldLabel);
            for (int i = 0; i < node.Characters.Count; i++)
            {
                var element = node.Characters[i];
                EditorGUILayout.BeginVertical();
                element.Name = EditorGUILayout.TextField("Name", element.Name);
                element.Type =  (CharactersType)EditorGUILayout.EnumPopup("Type", element.Type);
                if (GUILayout.Button("Remove"))
                {
                    node.Characters.RemoveAt(i);
                }
                EditorGUILayout.EndVertical();
            }

            // Кнопка для добавления нового элемента
            if (GUILayout.Button("Add Element"))
            {
                node.Characters.Add(new Character());
            }
        }
    }
}
#endif