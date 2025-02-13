using DialogueSystem.Enums;

using UnityEngine;
using System;
using System.Linq;
using UnityEditor.SceneManagement;



#if UNITY_EDITOR

using UnityEditor;
using XNodeEditor;
#endif
namespace DialogueSystem.Nodes.Editors
{
#if UNITY_EDITOR

    [CustomNodeEditor(typeof(DataNode))]
    public class DataNodeEditor : NodeEditor
    {
        // Пример выбранной метки (значение по умолчанию)
        private NovelTypes.Prefab lastPrefabType = 0;
        private NovelTypes.Prefab[] PrefabTypes;
        private int index = 0;
        private int hashOfLastPrefab = 0;
        private string[] GUIContents;

        public override void OnCreate()
        {
            if (target is not DataNode node) return;

            lastPrefabType = node.PrefabType;

            PrefabTypes = (NovelTypes.Prefab[])Enum.GetValues(typeof(NovelTypes.Prefab));

            GUIContents = PrefabTypes
                .Where(type =>
                    NovelTypes.GetType(type) != null && // Проверка на null для результата GetType
                    NovelTypes.GetType(type).IsSubclassOf(typeof(UnityEngine.Object)))
                .Select(type => type.ToString()).ToArray();

            index = Array.IndexOf(PrefabTypes, node.PrefabType);
        }

        public override void OnBodyGUI()
        {
            base.OnBodyGUI();

            if (target is not DataNode node)
            {
                Debug.LogWarning("Null target node for node editor!");
                return;
            }
            DrawFreeMarksMenu(node);
            DrawPrefabField(node);
        }

        private void DrawFreeMarksMenu(DataNode node)
        {
            index = EditorGUILayout.Popup(index, GUIContents);

            node.PrefabType = PrefabTypes[index];

            if (node.PrefabType != lastPrefabType)
            {
                node.Remove(lastPrefabType);
                node.Prefab = null;
            }

            lastPrefabType = node.PrefabType;
        }

       
        private void DrawPrefabField(DataNode node)
        {

            var type = NovelTypes.GetType(node.PrefabType);

            if (type == null) return;

            GUILayout.BeginHorizontal();

            node.Prefab = EditorGUILayout.ObjectField(node.Prefab, type, allowSceneObjects: true);

            GUILayout.EndHorizontal();

            int hash = node.Prefab?.GetHashCode() ?? 0;

            if (hashOfLastPrefab != hash)
            {
                node.Set(node.PrefabType, node.Prefab);
            }

            hashOfLastPrefab = hash;

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
