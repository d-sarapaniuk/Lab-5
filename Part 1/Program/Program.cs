using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Xml.Serialization;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = "C:\\Users\\user\\Desktop\\НАУ\\OOP 2\\Laboratory 3\\Part 1\\objects_data";

            String[] arrayIN = new String[] 
            { 
                new String("apple"), 
                new String("banana"), 
                new String("mandarin")
            };
            Serializer<String[]>.SerializeBinary(arrayIN, filepath);
            String[] arrayOUT = Serializer<String[]>.DeserializeBinary(filepath);

            List<String> listIN = new List<String>(arrayIN);
            Serializer<List<String>>.SerializeXML(listIN, filepath);
            List<String> listOUT = Serializer<List<String>>.DeserializeXML(filepath);

            List<String> queueIN = new List<String>(listIN);
            Serializer<List<String>>.SerializeJSON(queueIN, filepath);
            List<String> queueOUT = Serializer<List<String>>.DeserializeJSON(filepath);

            filepath = "C:\\Users\\user\\Desktop\\НАУ\\OOP 2\\Laboratory 3\\Part 1\\custom";
            // custom serialization call
            String strIN = new String("orange");
            Serializer<String>.SerializeXML(strIN, filepath); 
            String strOUT = Serializer<String>.DeserializeXML(filepath);
        }

    }
    public static class Serializer<T>
    {
        public static void SerializeBinary(T obj, string file)
        {
            using (FileStream fs = new FileStream(file + ".bin", FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
            }
        }
        public static T DeserializeBinary(string file)
        {
            T data;
            using (FileStream fs = new FileStream(file + ".bin", FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                data = (T)formatter.Deserialize(fs);
            }
            return data;
        }
        public static void SerializeJSON(T obj, string file)
        {
            using (FileStream fs = new FileStream(file + ".json", FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                jsonSerializer.WriteObject(fs, obj);
            }
        }
        public static T DeserializeJSON(string file)
        {
            T data;
            using (FileStream fs = new FileStream(file + ".json", FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                data = (T)jsonSerializer.ReadObject(fs);
            }
            return data;
        }
        public static void SerializeXML(T obj, string file)
        {
            using (FileStream fs = new FileStream(file + ".xml", FileMode.OpenOrCreate))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(fs, obj);
            }
        }
        public static T DeserializeXML(string file)
        {
            T data;
            using (FileStream fs = new FileStream(file + ".xml", FileMode.OpenOrCreate))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                data = (T)xmlSerializer.Deserialize(fs);
            }
            return data;
        }


    }
}