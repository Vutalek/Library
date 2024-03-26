using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary
{
    public static class BasicSerializer<T>
    {
        public static void SaveObject(string path, T FileToSave)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(fs, FileToSave);
            }
        }
        public static T LoadObjectFromFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            T FileFrom;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileFrom = (T)bf.Deserialize(fs);
            }
            catch(SerializationException)
            {
                throw;
            }
            finally
            {
                fs.Close();
            }
            return FileFrom;
        }
    }
}
