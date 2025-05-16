using DialogueSystem.Dictionary;
using DialogueSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace DialogueSystem.Models
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Dialogue System/Character")]
    [Serializable]
    public class Character : ScriptableObject
    {
        [SerializeField] private string _name = string.Empty;
        [SerializeField] private CharacterType _type;
        [SerializeField] private EmotionState _emotionState;

        public string Name => _name;
        public CharacterType Type => _type;
        public Emotion CurrentEmotion => _emotionState.CurrentEmotion;
        public Sprite CurrentSprite => _emotionState.CurrentSprite;

        // Метод для изменения эмоции
        public void ChangeEmotion(Emotion emotion)
        {
            _emotionState.SetEmotion(emotion);
        }

        public void OnValidate()
        {
            _emotionState.Validate();
        }

        [Serializable]
        public class EmotionState
        {
            [SerializeField] private Emotion _currentEmotion;
            [SerializeField] private List<SerializableDictionary<Emotion, Sprite>.SerializableKeyValuePair> _emotionList = new();

            public Emotion CurrentEmotion => _currentEmotion;
            public Sprite CurrentSprite => GetEmotionSprite(_currentEmotion);

            private SerializableDictionary<Emotion, Sprite> _emotionSprites;
            
            private bool init;

            private Sprite GetEmotionSprite(Emotion emotion)
            {
                if (!init) Init();

                if(_emotionSprites.TryGetValue(emotion, out var sprite)) return sprite;

                Debug.LogError($"There is no sprite for this emotion: {emotion} in the dictionary.");

                return null;

            }

            private void Init()
            {
                _emotionSprites = new SerializableDictionary<Emotion, Sprite>(_emotionList);
                init = true;
            }

            public void SetEmotion(Emotion emotion)
            {
                if (!init) Init();

                if (_emotionSprites.ContainsKey(emotion))
                {
                    _currentEmotion = emotion;
                }
                else
                {
                    Debug.LogWarning($"Эмоция {emotion} не найдена для персонажа.");
                }
            }

            internal void Validate()
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
        
        [Serializable]
        [XmlInclude(typeof(RelativePosition))]
        public abstract class Position
        {
            public PositioningType positioningType;

            protected Position() { }

            public Position(PositioningType type)
            {
                positioningType = type;
            } 
            // Было: enum Type { Relative, Absolute }
            // Стало: четкое название для типа позиционирования
            public enum PositioningType
            {
                Relative,
                Absolute
            }

            // Было: enum Relative { Left, Center, Right }
            // Стало: название, отражающее горизонтальную ориентацию
            public enum HorizontalAlignment
            {
                Left,
                Center,
                Right,
            }

            // Пример использования в классе
        }

        [Serializable]
        public class RelativePosition : Position
        {

            // Убираем readonly для сериализации
            public HorizontalAlignment Position;

            // Пустой конструктор для сериализации
            public RelativePosition() : base(PositioningType.Relative) { }

            public RelativePosition(HorizontalAlignment position ) : base(PositioningType.Relative)
            {
                Position = position;
            }
        }
    }
}
