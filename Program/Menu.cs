using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DAL;

namespace PL
{
    public class Menu
    {
        private int selectedOption = 0;
        private string[] fileType = { "Binary", "JSON", "XML" };
        private string[] entityOptions = { "Add", "Remove", "Display database" };
        private string[] entityTypes = { "Student", "Seller", "Gardener" };
        EntityService Service;

        private delegate void Delegate();
        Delegate menuDelegate;

        public Menu()
        {
            MainMenu();
        }
        public void MainMenu()
        {
            int selectedType = getSelectedOption(fileType, "Choose the type of the file you want to serialize to:");
            string enteredName = getFileName();
            Service = new EntityService(enteredName, (FileExtension)selectedType);
            EntityMenu();
        }
        private string getFileName()
        {
            Console.WriteLine("Enter the name of your file:");
            string? input = Console.ReadLine();
            return input == null? "database" : input;
        }
        private void EntityMenu()
        {
            menuDelegate = EntityMenu;
            selectedOption = 0;
            Console.Clear();
            int selected = getSelectedOption(entityOptions, "What would you like to do?");
            switch(selected)
            {
                case 0: AddingMenu(); break;
                case 1: RemovingMenu(); break;
                case 2: DisplayDatabase(); break;
            }
        }
        private void AddingMenu()
        {
            selectedOption = 0;
            Console.Clear();
            int selected = getSelectedOption(entityTypes, "Who would you like to add?");
            switch (selected)
            {
                case 0: addStudent(); break;
                case 1: addSeller(); break;
                case 2: addGardener(); break;
            }
        }
        private void RemovingMenu()
        {
            selectedOption = 0;
            Console.Clear();
            if (Service.GetEntities().Count == 0)
            {
                Console.WriteLine("There are no people in database to remove!");
                return;
            }
            string[] people = new string[Service.GetEntities().Count];
            int i = 0;
            foreach (Person person in Service.GetEntities())
            {
                people[i] = person.ToString();
                i++;
            }
            int selected = getSelectedOption(people, "Who would you like to remove?");
            Service.Remove(Service.GetEntities()[selected]);
            returnESC();
        }
        private void DisplayDatabase()
        {
            var list = Service.GetEntities();
            foreach(Person person in list)
            {
                Console.WriteLine(person);
            }
            returnESC();
        }
        private void addStudent()
        {
            Student student = new Student();
            student.First_Name = getFromUser(RegexCheck.Mode.FirstName);
            student.Last_Name = getFromUser(RegexCheck.Mode.LastName);
            student.Gender = getFromUser(RegexCheck.Mode.Gender);
            student.Course = Convert.ToInt16(getFromUser(RegexCheck.Mode.Course));
            student.Student_card = getFromUser(RegexCheck.Mode.StudentCard);
            student.Dormitory = getFromUser(RegexCheck.Mode.Dormitory) == "yes" ? true : false;
            student.Place_of_residence = student.Dormitory == true ? getFromUser(RegexCheck.Mode.ResidenceDorms) : getFromUser(RegexCheck.Mode.ResidenceDefault);

            Service.Add(student);
            Console.WriteLine("\nA new student was successfully added to the database!");
            returnESC();
        }
        
        private void addSeller()
        {
            Seller seller = new Seller();
            seller.First_Name = getFromUser(RegexCheck.Mode.FirstName);
            seller.Last_Name = getFromUser(RegexCheck.Mode.LastName);
            seller.Gender = getFromUser(RegexCheck.Mode.Gender);
            Service.Add(seller);
            Console.WriteLine("\nA new seller was successfully added to the database!");
            returnESC();
        }
        private void addGardener()
        {
            Gardener gardener = new Gardener();
            gardener.First_Name = getFromUser(RegexCheck.Mode.FirstName);
            gardener.Last_Name = getFromUser(RegexCheck.Mode.LastName);
            gardener.Gender = getFromUser(RegexCheck.Mode.Gender);
            Service.Add(gardener);
            Console.WriteLine("\nA new gardener was successfully added to the database!");
            returnESC();
        }
        private string getFromUser(RegexCheck.Mode inputType)
        {
            string? input;
            do
            {
                Console.Write("Enter " + RegexCheck.display[inputType] + ":  ");
                input = Console.ReadLine();

            } while (!RegexCheck.IsValid(inputType, input));
            return input;
        }
        private void displayOptions(string[] options, string title)
        {
            Console.WriteLine(title);
            for (int i = 0; i < options.Length; i++)
            {
                string prefix;
                if (i == selectedOption)
                {
                    prefix = " > ";
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    prefix = "   ";
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(prefix + options[i]);
            }
            Console.ResetColor();
        }
        private int getSelectedOption(string[] options, string title = "Choose an option using ↑↓ and ENTER:")
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                displayOptions(options, title);

                keyPressed = Console.ReadKey(true).Key;
                if (keyPressed == ConsoleKey.DownArrow)
                {
                    selectedOption++;
                    if (selectedOption == options.Length) selectedOption = 0;
                }
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    selectedOption--;
                    if (selectedOption == -1) selectedOption = options.Length - 1;
                }
            } while (keyPressed != ConsoleKey.Enter);
            return selectedOption;
        }
        private void returnESC()
        {
            Console.WriteLine("\n   [ESC] to return");
            ConsoleKey keyPressed;
            do
            {
                keyPressed = Console.ReadKey(true).Key;
            } while (keyPressed != ConsoleKey.Escape);
            menuDelegate();
        }

        private void exit()
        {
            Environment.Exit(0);
        }
    }
}
