using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueSystem.Dictionary.Utility
{
    static class DictionaryUtility
    {
        public static bool CheckForKeyCollisions<TKey, TValue>(IDictionary<TKey, TValue> dict1, IDictionary<TKey, TValue> dict2)
        {
            // Перебираем ключи первого словаря
            foreach (var key in dict1.Keys)
            {
                // Проверяем, есть ли этот ключ во втором словаре
                if (dict2.ContainsKey(key))
                {
                    return true; // Коллизия найдена
                }
            }

            return false; // Коллизий нет
        }


        public static IDictionary<TKey, TValue> MergeDictionaries<TKey, TValue>( 
            this IDictionary<TKey, TValue> dict1,
            IDictionary<TKey, TValue> dict2)
        {
            // Создаем новый словарь для объединения
            var mergedDict = new Dictionary<TKey, TValue>(dict1);

            // Добавляем элементы из второго словаря
            foreach (var kvp in dict2)
            {
                mergedDict[kvp.Key] = kvp.Value; // Перезаписываем значение, если ключ уже существует
            }

            return mergedDict;
        }
        public static IDictionary<TKey, TValue> MergeDictionariesRef<TKey, TValue>(
            IDictionary<TKey, TValue> to,
            IDictionary<TKey, TValue> from)
        {
            to ??= new Dictionary<TKey, TValue>();

            foreach (var kvp in from)
            {
                to[kvp.Key] = kvp.Value; // Перезаписываем значение, если ключ уже существует
            }

            return to;
        }
        public static void RemoveEmpty<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            var emptyValue = dict
                .Where(pair => pair.Value == null)
                .Select(pair => pair.Key);
            foreach (var kvp in emptyValue)
            {
                dict.Remove(kvp);
            }
        }
    }
}
