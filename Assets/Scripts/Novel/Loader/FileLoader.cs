using Antlr4.Runtime.Misc;
using System.IO;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Novel.Loader
{
    internal static class FileLoader
    {
        public const string ScriptFolder = "Scripts";
        public const string ImageFolder = "Images";
        private static SynchronizationContext _synchronizationContext;
        
        public static void Init()
        {
            _synchronizationContext = SynchronizationContext.Current;
        }

        public static string LoadScript(string scriptName)
        {
            var fullPath = GetPath(ScriptFolder, scriptName);
            var script = File.ReadAllText(fullPath);

            return script;
        }

        public static Texture2D LoadImage(string fileName)
        {
            var fullPath = GetPath(ScriptFolder, fileName);

            if (!File.Exists(fullPath)) return null;

            byte[] bytes = File.ReadAllBytes(fullPath);
            Texture2D texture = new Texture2D(1,1);
            texture.LoadImage(bytes);
            return texture;
        }

        public static string GetPath(string folder, string fileName)
        {
            string path = "";
            _synchronizationContext.Send(_ =>
            {
                path = Path.Combine(Application.persistentDataPath, folder, fileName);
            }, null);
            return path;
        }
    }
}
