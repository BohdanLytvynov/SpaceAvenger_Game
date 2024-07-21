using Newtonsoft.Json;

namespace JsonDataProvider
{
    public class JDataProvider
    {
        #region Fields

        public static string pathToEnv { get; }

        #endregion

        static JDataProvider()
        {
            pathToEnv = Environment.CurrentDirectory;
        }

        #region Ctor
        public JDataProvider()
        {
            
        }
        #endregion

        #region Methods

        public bool FileExists(string path)
        { 
            return File.Exists(path);
        }

        public void CreateFile(string path)
        {
            if (!FileExists(path))
            {
                var fs = File.Create(path);

                fs.Close();

                fs.Dispose();
            }
        }

        public void SerializeObject(string path, object obj)
        {
            CreateFile(path);

            JsonConvert.SerializeObject(obj, Formatting.None);
        }

        public T DeserializeObject<T>(string path) where T : new ()
        {
            if (FileExists(path))
            {
                return JsonConvert.DeserializeObject<T>(path) ?? new T();
            }

            return new T();
        }

        #endregion
    }
}