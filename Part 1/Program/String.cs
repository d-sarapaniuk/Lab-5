using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    [Serializable]
    public class String : ISerializable
    {
        public string? Value { get; set; }
        public int Length { get; set; }
        public String() { }
        public String(string value)
        {
            Value = value;
            Length = value.Length;
        }
        protected String(SerializationInfo info,  StreamingContext context)
        {
            Value = info.GetString("Value");
        }

        public int Search(string substring) // via KMP algorithm
        {
            if (substring.Length > Length) return -1;

            int[] lps = new int[substring.Length];  //longest prefix suffix array
            lps[0] = 0;
            int prevLPS = 0, i = 1;
            while (i < substring.Length)
            {
                if (substring[i] == substring[prevLPS])
                {
                    prevLPS++;
                    lps[i] = prevLPS;
                    i++;
                }
                else if (prevLPS == 0)
                {
                    lps[i] = 0;
                    i++;
                }
                else
                {
                    prevLPS = lps[prevLPS - 1];
                }
            }

            int j = 0; i = 0;
            while (i < Length)
            {
                if (Value[i] == substring[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    if (j == 0) i++;
                    else j = lps[j - 1];
                }
                if (j == substring.Length) return i - substring.Length;
            }
            return -1;
        }

        public void ReplaceSubstring(string substring, string replaceWith)
        {
            int index = Search(substring);
            if (index == -1) return;
            string result = "";
            int i = 0;

            while (i < index)
            {
                result += Value[i];
                i++;
            }
            result += replaceWith;
            i += substring.Length;
            while (i < Length)
            {
                result += Value[i];
                i++;
            }

            Value = result;
        }
        public override string ToString()
        {
            return $"String: {Value}, Length: {Length}"; ;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value);
            Console.WriteLine($"{this} was called to serialize");
        }
    }

}
