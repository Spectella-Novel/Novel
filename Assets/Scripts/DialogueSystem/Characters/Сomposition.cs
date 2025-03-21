using DialogueSystem.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.Characters
{
    [Serializable]
    public class Composition
    {
        [SerializeField] public SerializableDictionary<Character.Position, Character> _dictionary;
        
        public Character Main; 
        
        public ICollection<Character.Position> Positions => _dictionary.Keys;
        public Character Get(Character.Position position) => _dictionary[position];
        public Character Set(Character.Position position, Character character) => _dictionary[position] = character;

        public Composition() { }
        public Composition(Character Main)
        {
            _dictionary = new();
            if (Main.Type == Enums.CharacterType.Minor) Debug.Log($"{Main} is not main character");
            this.Main = Main;
        }
    }
}
