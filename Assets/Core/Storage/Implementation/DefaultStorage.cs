using Core.Shared;
using Core.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Storage.Implementation
{
    public class DefaultStorage : IStorage
    {
        public async Task<Result<T>> Restore<T>(string tag)
        {
            try
            {
                string fullPath = Path.Combine(Application.persistentDataPath, tag + ".json");
                if (!File.Exists(fullPath)) return Result<T>.Failure("File not exist");

                string json = await File.ReadAllTextAsync(fullPath, Encoding.UTF8);
                var data = JsonUtility.FromJson<T>(json);
                return Result<T>.Success(data);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Ошибка загрузки: {e.Message}");
                return Result<T>.Failure($"Error of restore: {e.Message}");
            }
        }

        public async Task<Result> Save<T>(string tag, T value)
        {
            try
            {
                string fullPath = Path.Combine(Application.persistentDataPath, tag + ".json");
                string directoryPath = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                string json = JsonUtility.ToJson(value, true);
                await File.WriteAllTextAsync(fullPath, json, Encoding.UTF8);
                return Result.Success();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Ошибка сохранения: {e.Message}");
                return Result.Failure(e.Message);
            }
        }
    }
}
