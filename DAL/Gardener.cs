using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Serializable]
    public class Gardener : Person
    {
        public int TreesPlanted { get; set; } = 0;

        public Gardener()
        {
            
        }
        public Gardener(string first_Name, string last_Name, string gender) : base(first_Name, last_Name, gender)
        {
            
        }

        public override string ToString()
        {
            return String.Format($"Gardener {First_Name} {Last_Name}, {Gender}, planted {TreesPlanted} trees");
        }
        
    }
}
