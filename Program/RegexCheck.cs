using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PL
{
    public static class RegexCheck
    {
        public enum Mode
        {
            FirstName,
            LastName,
            Gender,
            Course,
            StudentCard,
            Dormitory,
            ResidenceDefault,
            ResidenceDorms,            

        }
        public static Dictionary<Mode, string> display = new Dictionary<Mode, string>()
        {
            {Mode.FirstName, "a first name"},
            {Mode.LastName, "a last name" },
            {Mode.Gender, "a gender" },
            {Mode.Course, "a course" },
            {Mode.StudentCard, "a student card number" },
            {Mode.Dormitory, "if a student lives in a dormitory"},
            {Mode.ResidenceDefault, "a residence" },
            {Mode.ResidenceDorms, "a dormitory and room number" }
        };

        public static bool IsValid(Mode mode, string value)
        {
            string format;
            switch(mode)
            {
                case Mode.FirstName: 
                    format = @"^\p{L}{1,32}$";
                    break;
                case Mode.LastName:
                    format = @"^\p{L}{1,32}$";
                    break;
                case Mode.Gender:
                    format = @"male|female";
                    break;
                case Mode.Course: 
                    format = @"^[1-6]$";
                    break;
                case Mode.StudentCard:
                    format = @"[A-Z]{2}\d{8}";
                    break;
                case Mode.Dormitory:
                    format = @"yes|no";
                    break;
                case Mode.ResidenceDefault:
                    format = @"^\p{L}{1,50}$";
                    break;
                case Mode.ResidenceDorms:
                    format = @"\d+\.\d{3}";
                    break;
                default:
                    format = "";
                    break;
            }
            Regex regex = new Regex(format);
            return regex.IsMatch(value);

        }
        

    }
}
