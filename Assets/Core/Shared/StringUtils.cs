using System.Text;
using UnityEngine;


namespace Core.Shared
{
    public static class StringUtils
    {
        /// <summary>
        /// Генерирует случайную строку заданной длины.
        /// </summary>
        /// <param name="length">Длина строки</param>
        /// <param name="useUppercase">Использовать заглавные буквы</param>
        /// <param name="useLowercase">Использовать строчные буквы</param>
        /// <param name="useDigits">Использовать цифры</param>
        /// <param name="useSpecialChars">Использовать специальные символы</param>
        /// <returns>Случайная строка</returns>
        public static string GenerateRandomString(
            int length = 10,
            bool useUppercase = true,
            bool useLowercase = true,
            bool useDigits = true,
            bool useSpecialChars = false)
        {
            if (length <= 0)
                return string.Empty;

            StringBuilder pool = new StringBuilder();

            if (useUppercase)
                pool.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            if (useLowercase)
                pool.Append("abcdefghijklmnopqrstuvwxyz");
            if (useDigits)
                pool.Append("0123456789");
            if (useSpecialChars)
                pool.Append("!@#$%^&*()_+-=[]{}|;:,.<>?");

            if (pool.Length == 0)
                return string.Empty;

            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = Random.Range(0, pool.Length);
                result.Append(pool[index]);
            }

            return result.ToString();
        }
    }
}
