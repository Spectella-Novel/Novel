using DialogueSystem.Enums;
using UnityEditor;
using UnityEngine;

namespace DialogueSystem.Nodes
{
    public abstract class DataNodeBase : NodeBase<NovelTypes.Prefab>
    {
        [Output] public GameObject Output;
        [Input] public GameObject Input;
    }
}
