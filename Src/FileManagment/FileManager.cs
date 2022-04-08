using System.IO;
using Newtonsoft.Json;

namespace TileBasedPlatformer.Src
{
    public class FileManager<T> where T : new()
    {
        private readonly string fullPath;

        public FileManager(string fileName)
        {
            fullPath = Path.GetFullPath(fileName);
        }

        public T ReadData()
        {
            T data;

            using(StreamReader file = File.OpenText(fullPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                data = (T)serializer.Deserialize(file, typeof(T));
            }

            return data;
        }

        public void WriteData(T data)
        {
            using (StreamWriter file = File.CreateText(fullPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, data);
            }
        }
    }
}
