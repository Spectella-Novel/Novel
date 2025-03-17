using DialogueSystem.Data;
using DialogueSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Dialogue System/Character")]
    [Serializable]
    public class Character : ScriptableObject
    {
        [SerializeField]
        public string Name = string.Empty;

        [SerializeField, HideInInspector]
        public SerializableDictionary<Emotion, Sprite> Emotions;

        public CharacterType Type => type;

        [SerializeField]
        private List<SerializableDictionary<Emotion, Sprite>.SerializableKeyValuePair> _emotionList = new();

        [SerializeField]
        private CharacterType type;

        private void OnValidate()
        {
            if (_emotionList.Count == 0)
            {
                return;
            }

            FixLastEmotionConflict();
            RemoveDuplicateEmotions();
        }

        private void FixLastEmotionConflict()
        {
            var lastIndex = _emotionList.Count - 1;
            var lastPair = _emotionList[lastIndex];

            var duplicatesCount = _emotionList.Count(pair => pair.Key == lastPair.Key);
            if (duplicatesCount <= 1)
            {
                return;
            }

            var busyEmotions = new HashSet<Emotion>(_emotionList.Select(pair => pair.Key));
            var allEmotions = (Emotion[])Enum.GetValues(typeof(Emotion));
            var freeEmotions = allEmotions.Except(busyEmotions).ToList();

            if (freeEmotions.Any())
            {
                lastPair.Key = freeEmotions.First();
                _emotionList[lastIndex] = lastPair;
            }
            else
            {
                _emotionList.RemoveAt(lastIndex);
            }
        }

        private void RemoveDuplicateEmotions()
        {
            _emotionList = _emotionList
                .GroupBy(pair => pair.Key)
                .Select(group => group.First())
                .ToList();
        }
    }
}
