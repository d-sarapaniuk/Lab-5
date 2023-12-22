using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DAL
{
    public interface IDataProvider<T>
    {
        void Serialize(T obj, string file);
        T Deserialize(string file);
        string GetExtension();
    }

#pragma warning disable SYSLIB0011
    public class BinaryProvider<T> : IDataProvider<T>
    {
        private BinaryFormatter formatter = new BinaryFormatter();
        public string GetExtension() => ".bin";
        public T Deserialize(string file)
        {
            T data;
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                data = (T)formatter.Deserialize(fs);
            }
            return data;
        }

        public void Serialize(T obj, string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, obj);
            }
        }
    }
    public class JSONProvider<T> : IDataProvider<T>
    {
        public string GetExtension() => ".json";
        public T Deserialize(string file)
        {
            T data;
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                data = (T)JsonSerializer.Deserialize(fs, typeof(T));
            }
            return data;
        }

        public void Serialize(T obj, string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, obj);
            }
        }
    }
    public class XMLProvider<T> : IDataProvider<T>
    {
        private XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        public string GetExtension() => ".xml";
        public T Deserialize(string file)
        {
            T data;
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                data = (T)xmlSerializer.Deserialize(fs);
            }
            return data;
        }

        public void Serialize(T obj, string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, obj);
            }
        }
    }
}
