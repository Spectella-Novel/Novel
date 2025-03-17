#if UNITY_EDITOR
using DialogueSystem.Enums;
using DialogueSystem.Nodes.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
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
            var graph = node.graph as DSGraph;

            if (node == null) return;

            // Отображаем стандартные поля узла
            base.OnBodyGUI();
            if(node.Characters == null) return;
            // Отображаем список элементов
            EditorGUILayout.LabelField("Elements", EditorStyles.boldLabel);
            for (int i = 0; i < node.Characters.Count; i++)
            {
                
                var element = node.Characters[i];
                
                if(element == null) continue;

                EditorGUILayout.BeginVertical();

                if(element.Name != null) EditorGUILayout.LabelField(element.Name);
                EditorGUILayout.LabelField(element.Type.ToString());
                node.Characters[i] = EditorGUILayout.ObjectField(element, typeof(Character), false) as Character;
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