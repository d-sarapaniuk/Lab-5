using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;

namespace DAL
{
    [JsonDerivedType(typeof(Gardener), "Gardener")]
    [JsonDerivedType(typeof(Seller), "Seller")]
    [JsonDerivedType(typeof(Student), "Student")]
    [Serializable]
    [XmlInclude(typeof(Gardener))]
    [XmlInclude(typeof(Seller))]
    [XmlInclude(typeof(Student))]
    public class Person : IXmlSerializable
    {
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Gender { get; set; }

        public IWritePoemsBehaviour writePoemsBehaviour = new CantWritePoems();
        public Person() { }
        public Person(string first_Name, string last_Name, string gender)
        {
            First_Name = first_Name;
            Last_Name = last_Name;
            Gender = gender;
        }
        public virtual void performWritePoem()
        {
            writePoemsBehaviour.Write();
        }      

        public void ReadXml(XmlReader reader)
        {
            if (!reader.HasAttributes)
                throw new Exception("Something went wrong");
            if (reader.MoveToAttribute("FirstName") && reader.ReadAttributeValue())
                First_Name = reader.Value;
            if (reader.MoveToAttribute("LastName") && reader.ReadAttributeValue())
                Last_Name = reader.Value;
            if (reader.MoveToAttribute("WritePoemsBehaviour") && reader.ReadAttributeValue())
            {
                var typeToSet = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .Where(x => typeof(IWritePoemsBehaviour).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList()
                    .FirstOrDefault(x => x.Name == reader.Value);
                if (typeToSet == null)
                    throw new Exception("This type does not exist!");
                writePoemsBehaviour = (IWritePoemsBehaviour)Activator.CreateInstance(typeToSet);
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("FirstName", First_Name);
            writer.WriteAttributeString("LastName", Last_Name);
            writer.WriteAttributeString("Gender", Gender);
            writer.WriteAttributeString("WritePoemsBehaviour", writePoemsBehaviour.GetType().Name);
        }

        public XmlSchema? GetSchema()
        {
            return null;
        }
    }
}
