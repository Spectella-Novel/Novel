#if UNITY_EDITOR
using DialogueSystem.Models;
using DialogueSystem.Models.Enums;
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
            var graph = node.graph as DialogueGraph;

            if (node == null) return;

            // Отображаем стандартные поля узла
            base.OnBodyGUI();
            if(node.Characters == null) return;
            // Отображаем список элементов
            EditorGUILayout.LabelField("Elements", EditorStyles.boldLabel);
            for (int i = 0; i < node.Characters.Count; i++)
            {

                var element = node.Characters[i];

                if (element == null) continue;

                EditorGUILayout.BeginVertical();

                EditorGUILayout.LabelField($"Name: {element.Name}");
                EditorGUILayout.LabelField($"Type: {element.Type.ToString()}");

                if (element.Type == CharacterType.Minor)
                {
                    var currentEmotion = (Emotion)EditorGUILayout.EnumPopup(node.Characters[i].CurrentEmotion);
                    node.Characters[i].ChangeEmotion(currentEmotion);

                }

                node.Characters[i] = EditorGUILayout.ObjectField(element, typeof(Character), false) as Character;

                if (GUILayout.Button("Remove")) node.Characters.RemoveAt(i);
                
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