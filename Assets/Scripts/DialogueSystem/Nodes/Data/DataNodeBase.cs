using DialogueSystem.Enums;
using UnityEditor;
using UnityEngine;

namespace DialogueSystem.Nodes.Data
{
    public abstract class DataNodeBase : NovelNode
    {
        [Output] public GameObject Output;
        [Input] public GameObject Input;
    }
}
