

using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AnilTools.Save
{
    public static partial class JsonManager
    {
        public static void Save<T>(T obj, string path , string name)
        {
            CheckPath(path);

            string konum = Path.Combine(SavesFolder + path + name);
            string saveText = JsonUtility.ToJson(obj, true);

            File.WriteAllText(konum, saveText);
        }

        public static T Load<T>(string path, string name)
        {
            T objectToLoad = default;
            
            CheckPath(path);
            string konum = Path.Combine(SavesFolder + path + name);

            if (File.Exists(konum))
            {
                string loadText = File.ReadAllText(konum);
                objectToLoad = JsonUtility.FromJson<T>(loadText);
            }
            
            return objectToLoad;
        }

        public static void SaveList<T>(string path, string name, List<T> obj)
        {
            if (Directory.Exists(SavesFolder + path))
            {
                Directory.Delete(SavesFolder + path, true);
            }

            CheckPath(path);

            for (int i = 0; i < obj.Count; i++)
            {
                string konum = Path.Combine(SavesFolder + path + i.ToString() + name);
                string saveText = JsonUtility.ToJson(obj[i], true);

                File.WriteAllText(konum, saveText);
            }
        }

        public static List<T> LoadList<T>(string path, string name)
        {
            var objectToLoad = new List<T>();

            CheckPath(path);

            DirectoryInfo directoryInfo = new DirectoryInfo(SavesFolder + path);

            int count = directoryInfo.GetFiles().Length;

            for (int i = 0; i < count; i++)
            {
                string konum = Path.Combine(SavesFolder + path + i.ToString() + name);
                if (File.Exists(konum))
                {
                    string loadText = File.ReadAllText(konum);
                    objectToLoad.Add(JsonUtility.FromJson<T>(loadText));
                }
            }

            return objectToLoad;
        }

    }
}
