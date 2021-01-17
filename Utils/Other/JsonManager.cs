#pragma warning disable IDE0059 // Unnecessary assignment of a value
// https://github.com/Cysharp/UniTask
// bu özelliği aktif etmek için yukarıdaki asseti indiriniz
// aksi taktirde allttaki satırı commnt satırı yapınız
#define HasCysharpAsset
#if HasCysharpAsset
using Cysharp.Threading.Tasks;
#endif
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AnilTools.Save
{
    public static partial class JsonManager
    {
        public static string SavesFolder => Application.persistentDataPath + "/Saves/";
#if HasCysharpAsset

        #region Save
        /// <summary>
        /// objeyi kaydetmeye yarar
        /// </summary>
        /// <typeparam name="T">Objenin türünü giriniz</typeparam>
        /// <param name="path">objenin save edileceği yer örn: meshler/</param>
        /// <param name="name">örn: /objeler.json</param>
        /// <param name="obj">struct , class yaparken zorunlu olay [system.serializable] eklemek </param>
        public async static UniTask SaveAsync<T>(string path, string name, T obj)
        {
            CheckPath(path);

            string konum = Path.Combine(SavesFolder + path + name);
            string saveText = JsonUtility.ToJson(obj, true);

            File.WriteAllText(konum, saveText);

            await UniTask.Yield();
        }

        // callback
        public async static UniTask SaveAsync<T>(this T obj, string path, string name)
        {
            await SaveAsync(path, name, obj);
        }

        /// <summary>
        /// objeyi kaydetmeye yarar
        /// </summary>
        /// <typeparam name="T">Objenin türünü giriniz</typeparam>
        /// <param name="path">objenin save edileceği yer örn: meshler/</param>
        /// <param name="name">örn: /objeler.json</param>
        /// <param name="obj">struct , class yaparken zorunlu olay [system.serializable] eklemek </param>
        public async static UniTask SaveListAsync<T>(string path, string name, List<T> obj)
        {
            var tasks = new List<UniTask>();

            if (Directory.Exists(SavesFolder + path))
            {
                Directory.Delete(SavesFolder + path, true);
            }

            CheckPath(path);

            for (int i = 0; i < obj.Count; i++)
            {
                tasks.Add(UniTask.Create(async () =>
                {
                    await UniTask.Yield();
                    string konum = Path.Combine(SavesFolder + path + i.ToString() + name);
                    string saveText = JsonUtility.ToJson(obj[i], true);

                    File.WriteAllText(konum, saveText);
                }));
            }

            await UniTask.WhenAll(tasks);
        }

        public async static UniTask SaveListAsync<T>(this List<T> obj, string path, string name)
        {
            await obj.SaveListAsync(path, name);
        }

        /// <summary>
        /// objeyi kaydetmeye yarar
        /// </summary>
        /// <typeparam name="T">Objenin türünü giriniz</typeparam>
        /// <param name="path">objenin save edileceği yer örn: meshler/</param>
        /// <param name="name">örn: /objeler.json</param>
        /// <param name="obj">struct , class yaparken zorunlu olay [system.serializable] eklemek </param>
        public async static UniTask SaveArrayAsync<T>(string path, string name, T[] obj)
        {
            var tasks = new List<UniTask>();

            if (Directory.Exists(SavesFolder + path))
            {
                Directory.Delete(SavesFolder + path, true);
            }

            CheckPath(path);

            for (int i = 0; i < obj.Length; i++)
            {
                tasks.Add(UniTask.Create(async () =>
                {
                    await UniTask.Yield();
                    string konum = Path.Combine(SavesFolder + path + i.ToString() + name);
                    string saveText = JsonUtility.ToJson(obj[i], true);
                    File.WriteAllText(konum, saveText);
                }));
            }

            await UniTask.WhenAll(tasks);
        }
        #endregion

        #region Loads
        /// <summary>
        /// objeyi yüklemeye yarar
        /// </summary>
        /// <typeparam name="T">Objenin türünü giriniz</typeparam>
        /// <param name="path">objenin Load edileceği yer örn: meshler</param>
        /// <param name="name">örn: /objeler.json ve .json  zorunlu</param>
        public async static UniTask<T> LoadAsync<T>(string path, string name)
        {
            T objectToLoad = default;
            var task = UniTask.Create(async () =>
            {
                await UniTask.Yield();
                CheckPath(path);
                string konum = Path.Combine(SavesFolder + path + name);

                if (File.Exists(konum))
                {
                    string loadText = File.ReadAllText(konum);
                    objectToLoad = JsonUtility.FromJson<T>(loadText);
                }
            });

            await UniTask.WhenAll(task);

            return objectToLoad;
        }

        public async static UniTask LoadAsync<T>(this T t, string path, string name)
        {
            T objectToLoad = default;

            CheckPath(path);

            string konum = Path.Combine(SavesFolder + path + name);

            if (File.Exists(konum))
            {
                string loadText = File.ReadAllText(konum);
                objectToLoad = JsonUtility.FromJson<T>(loadText);
            }

            await UniTask.Yield();

            t = objectToLoad;
        }

        /// <summary>
        /// objeyi yüklemeye yarar
        /// </summary>
        /// <typeparam name="T">Objenin türünü giriniz</typeparam>
        /// <param name="path">objenin Load edileceği yer örn: meshler</param>
        /// <param name="name">örn: /objeler.json ve .json  zorunlu</param>
        public async static UniTask<List<T>> LoadListAsync<T>(string path, string name)
        {
            var tasks = new List<UniTask>();
            var objectToLoad = new List<T>();

            CheckPath(path);

            DirectoryInfo directoryInfo = new DirectoryInfo(SavesFolder + path);

            int count = directoryInfo.GetFiles().Length;

            for (int i = 0; i < count; i++)
            {
                tasks.Add(UniTask.Create(async () =>
                {
                    await UniTask.Yield();
                    string konum = Path.Combine(SavesFolder + path + i.ToString() + name);
                    if (File.Exists(konum))
                    {
                        string loadText = File.ReadAllText(konum);
                        objectToLoad.Add(JsonUtility.FromJson<T>(loadText));
                    }
                }));
            }

            await UniTask.WhenAll(tasks);

            return objectToLoad;
        }

        /// <summary>
        /// objeyi yüklemeye yarar
        /// </summary>
        /// <typeparam name="T">Objenin türünü giriniz</typeparam>
        /// <param name="path">objenin Load edileceği yer örn: meshler</param>
        /// <param name="name">örn: /objeler.json ve .json  zorunlu</param>
        public async static UniTask<T[]> LoadArrayAsync<T>(string path, string name)
        {
            CheckPath(path);

            DirectoryInfo directoryInfo = new DirectoryInfo(SavesFolder + path);

            int count = directoryInfo.GetFiles().Length;

            T[] objectToLoad = new T[count];
            var tasks = new List<UniTask>();

            for (int i = 0; i < count; i++)
            {
                tasks.Add(UniTask.Create(async () =>
                {
                    await UniTask.Yield();
                    string konum = Path.Combine(SavesFolder + path + i.ToString() + name);
                    if (File.Exists(konum))
                    {
                        string loadText = File.ReadAllText(konum);
                        objectToLoad[i] = JsonUtility.FromJson<T>(loadText);
                        Debug2.Log(loadText);
                    }
                }));
            }

            await UniTask.WhenAll(tasks);

            return objectToLoad;
        }
        #endregion

        private static void CheckPath(string path)
        {
            if (!File.Exists(SavesFolder))
            {
                Directory.CreateDirectory(SavesFolder);
            }
            if (!File.Exists(SavesFolder + path))
            {
                Directory.CreateDirectory(SavesFolder + path);
            }
        }


#endif
    }
}