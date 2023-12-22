using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IWritePoemsBehaviour
    {
        void Write();
    }
    [Serializable]
    public class WriteFunnyPoem : IWritePoemsBehaviour
    {
        public void Write()
        {
            Console.WriteLine(" is writing a funny poem...");
        }
    }
    [Serializable]
    public class WriteSadPoem : IWritePoemsBehaviour
    {
        public void Write()
        {
            Console.WriteLine(" is writing a sad poem...");
        }
    }
    [Serializable]
    public class CantWritePoems : IWritePoemsBehaviour
    {
        public void Write()
        {
            Console.WriteLine(" can't write poems!");
        }
    }
}
