using DialogueSystem.Enums;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using XNodeEditor;
#endif
namespace DialogueSystem.Nodes.Editors
{
#if UNITY_EDITOR

    [CustomNodeEditor(typeof(ChangeNode))]
    public class ChangeNodeEditor : NodeEditor
    {
        // Пример выбранной метки (значение по умолчанию)

        public override void OnBodyGUI()
        {
            base.OnBodyGUI();

            if (target is not ChangeNode node)
            {
                Debug.LogWarning("Null target node for node editor!");
                return;
            }

            DrawPrefabField(node);
        }

        int hashOfLastPrefab = 0;
        private void DrawPrefabField(ChangeNode node)
        {

            var type = NovelTypes.GetType(node.PrefabType);

            if (type == null) return;

            GUILayout.BeginHorizontal();

            node.Prefab = EditorGUILayout.ObjectField(node.Prefab, type, allowSceneObjects: true);

            var hash = node.Prefab.GetHashCode();
            if (hashOfLastPrefab != hash)
            {
                node.Set(node.PrefabType, node.Prefab);
            }
            hashOfLastPrefab = hash;
            GUILayout.EndHorizontal();

            if (node.PrefabType == NovelTypes.Prefab.Sprite)
            {
                DrawImage(node.Get<Texture2D>(node.PrefabType), 180, 100);
            }
        }

        private static void DrawImage(Texture2D background, float width, float height)
        {
            if (background == null) return;

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(new GUIContent(background), GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
#endif
}
//НЕ УДАЛЯТЬ! РЕАЛИЩОВАННЫЙ ЭЛЕМЕНТ ПОИСКА

//private void DrawSelectionGrid(DataNode node, Rect TextFieldPosition)
//{
//    if (currentMark != lastMark) selectedGridOpen = true;
//    if (string.IsNullOrEmpty(currentMark) || !selectedGridOpen) return;

//    var options = node.dsGraph.Marks.Where(str => str.Contains(currentMark)).Take(4).ToList();
//    options.AddRange(node.dsGraph.DefaultMarks);
//    options.Add("Добавить свою");

//    TextFieldPosition.y += TextFieldPosition.height;
//    int elementHeigth = 20;
//    TextFieldPosition.height = elementHeigth * options.Count + 10;

//    selectedIndex = GUI.SelectionGrid(TextFieldPosition, selectedIndex, options.ToArray(), 1, listStyle);

//    if (selectedIndex == -1) return;

//    if (selectedIndex == options.Count - 1)
//    {
//        node.dsGraph.AddMarks(currentMark);
//    }
//    else
//    {
//        currentMark = options[selectedIndex];
//        node.Mark = currentMark;
//    }

//    selectedGridOpen = false;
//    lastMark = currentMark;
//    selectedIndex = -1;
//}
