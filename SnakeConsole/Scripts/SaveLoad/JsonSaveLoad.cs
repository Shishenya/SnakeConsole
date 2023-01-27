using SnakeConsole.Scripts.SaveLoad;
using System.IO;
using System.Text.Json;

namespace SnakeConsole
{
    public class JsonSaveLoad: ISaveable, ILoadable
    {
        private static string _fileName = "SaveData.json"; // имя файла сохранеия

        /// <summary>
        /// Сохраняет данные
        /// </summary>
        public void SaveData<T>(T data)
        {
            FileStream fs = new FileStream(_fileName, FileMode.Create);
            JsonSerializer.Serialize<T>(fs, data);
            fs.Close();
        }

        /// <summary>
        /// Загружает данные
        /// </summary>
        public T LoadData<T>(ref T data)
        {

           FileStream fs = new FileStream(_fileName, FileMode.OpenOrCreate);
           data = JsonSerializer.Deserialize<T>(fs);
           fs.Close();
           return data;           
        }

        /// <summary>
        /// Проверяет, есть ли файл сейва
        /// </summary>
        public static bool isBeenSave()
        {
            if (File.Exists( _fileName))
            {
                return true;
            }
            return false;
        }
    }
}
