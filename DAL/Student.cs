using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DAL
{
    [Serializable]
    public class Student : Person, IXmlSerializable
    {
        public int Course { get; set; }
        public string? Student_card { get; set; }
        public bool Dormitory { get; set; } = false;
        public string? Place_of_residence { get; set; }
        public Student()
        {
            writePoemsBehaviour = new WriteFunnyPoem();
        }
        public Student(string first_Name, string last_Name, string gender, int course, string student_card, bool dormitory, string place_of_residence)
            : base(first_Name, last_Name, gender)
        {
            this.Course = course;
            this.Student_card = student_card;
            this.Dormitory = dormitory;
            this.Place_of_residence = place_of_residence;
            writePoemsBehaviour = new WriteFunnyPoem();
        }

        public override string ToString()
        {
            string inDorms = Dormitory ? "lives in dormitory" : "DOESN'T live in dormitory";
            return String.Format($"Student {First_Name} {Last_Name}, {Gender}, year {Course}, {Student_card}, {inDorms}, {Place_of_residence}");
        }
        public void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
            writer.WriteAttributeString("Course", Course.ToString());
            writer.WriteAttributeString("StudentCard", Student_card);
            writer.WriteAttributeString("InDorms", Dormitory ? "yes" : "no");
            writer.WriteAttributeString("PlaceOfResidence", Place_of_residence);

        }
    }
}
