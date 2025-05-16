
using System;
using UnityEngine;

namespace DialogueSystem.Types.Reactive
{
    [System.Serializable]
    public class ReactiveProperty<T>
    {
        public T Value 
        {
            get => _value; 
            set {
                if (value == null) return;
                _value = value;
                OnChange?.Invoke(_value);
            } 
        }
        [SerializeField] protected T _value;
        public Action<T> OnChange;

        public void NotifyPropertyChanged()
        {
            OnChange?.Invoke(_value);
        }

    }
}
