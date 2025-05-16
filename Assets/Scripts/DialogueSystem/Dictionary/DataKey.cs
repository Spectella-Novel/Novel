using System;
using System.Collections.Generic;

namespace DialogueSystem.Dictionary
{
    /// <summary>
    /// Структура-ключ для словаря, состоящая из двух компонентов: типа и метки.
    /// Сделана неизменяемой и реализует IEquatable для корректного сравнения ключей.
    /// </summary>
    [Serializable]
    public readonly struct DataKey<T, V> : IEquatable<DataKey<T, V>>
    {
        // Используем свойства только для чтения для обеспечения иммутабельности.
        public T TypeKey { get; }
        public V Mark { get; }

        public DataKey(T typeKey, V mark)
        {
            TypeKey = typeKey;
            Mark = mark;
        }

        public bool Equals(DataKey<T, V> other)
        {
            return EqualityComparer<T>.Default.Equals(TypeKey, other.TypeKey) &&
                   EqualityComparer<V>.Default.Equals(Mark, other.Mark);
        }

        public override bool Equals(object obj)
        {
            return obj is DataKey<T, V> other && Equals(other);
        }

        public override int GetHashCode()
        {
            // Используем комбинирование хеш-кодов для повышения распределённости
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + (TypeKey != null ? TypeKey.GetHashCode() : 0);
                hash = hash * 31 + (Mark != null ? Mark.GetHashCode() : 0);
                return hash;
            }
        }
    }
}