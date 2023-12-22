using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Serializable]
    public class Seller : Person
    {
        public Seller()
        {
            writePoemsBehaviour = new WriteSadPoem();
        }
        public Seller(string first_Name, string last_Name, string gender) : base(first_Name, last_Name, gender)
        {
            writePoemsBehaviour = new WriteSadPoem();
        }

        public override string ToString()
        {
            return String.Format($"Seller {First_Name} {Last_Name}, {Gender}");
        }
    }
}
