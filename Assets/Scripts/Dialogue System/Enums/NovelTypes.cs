using System;
using UnityEngine;

namespace DialogueSystem.Enums
{
    public  class NovelTypes
    {
        public static Type GetType(Prefab prefabType)
        {
            return prefabType switch
            {
                Prefab.GameObject => typeof(GameObject),
                Prefab.Music => typeof(AudioClip),
                Prefab.Sprite => typeof(Texture2D),
                Prefab.Character => typeof(GameObject),
                _ => null
            };
        }
        public enum Prefab
        {
            TerminalNode = -1,
            GameObject = 0,
            Music = 1,
            Sprite = 2,
            Character = 3,
        }
    }

}