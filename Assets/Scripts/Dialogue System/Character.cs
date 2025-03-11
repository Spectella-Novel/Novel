using DialogueSystem.Enums;
using System;
using UnityEngine;
namespace DialogueSystem
{
    [Serializable]
    public class Character
    {
        [SerializeField] public string Name;
        [SerializeField] public CharactersType Type;

        public Character(){}

        public Character(string name, CharactersType type)
        {
            Name = name;
            Type = type;
        }
    }
}
