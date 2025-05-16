#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using XNodeEditor;
#endif


#if UNITY_EDITOR
namespace DialogueSystem
{
    [CustomEditor(typeof(DialogueGraph))]
    public class DialogueGraphEditor: GlobalNodeEditor
    {
        public override void OnInspectorGUI()
        {
            var graph =  target as DialogueGraph;

            if (graph == null) return;
            
            base.OnInspectorGUI();

            if (GUILayout.Button("Refresh node", GUILayout.Height(40)))
            {
                graph.RefreshData();
            }

        }
    }
}
#endif
