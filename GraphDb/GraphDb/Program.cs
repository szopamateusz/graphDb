using System;

namespace GraphDb
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DbOperations operations = new DbOperations();
            operations.Connect("http://localhost:7474/db/data","","");

 
            int choice;
            do
            {
                Console.WriteLine("Welcome to first shabby implementation of GraphDB in C#. Below are some basic operation which You can perform.");
                Console.WriteLine("1. Add person.");
                Console.WriteLine("2. Relate 2 people as friends.");
                Console.WriteLine("3. Show people and friends.");
                Console.WriteLine("4.Delete person.");
                Console.WriteLine("5.Edit personal information.");
                Console.WriteLine("6. Create group.");
                Console.WriteLine("7. Add person to group.");
                Console.WriteLine("8. Add post to group.");
                Console.WriteLine("9. Show posts in group. ");
                Console.WriteLine("10. Exit");
                Console.WriteLine("Choose operation");

                int.TryParse(Console.ReadLine(),out choice);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter name");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter surname");
                        string surname = Console.ReadLine();
                        Console.WriteLine("Enter date of birth in format yyyy-MM-dd");
                        string dateOfBirth = Console.ReadLine();
                        Console.WriteLine("Enter speciality");
                        string speciality = Console.ReadLine();
                        Console.WriteLine("Enter group");
                        string group = Console.ReadLine();
                        operations.AddPerson(name,surname,dateOfBirth,speciality,group);
                        break;
                    case 2:
                        int id1, id2;
                        Console.WriteLine("Enter ID of first person");
                        int.TryParse(Console.ReadLine(), out id1);
                        Console.WriteLine("Enter ID of second person");
                        int.TryParse(Console.ReadLine(), out id2);
                        operations.RelatePeople(id1,id2);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    default:
                        break;
                }
         
            } while (choice != 10);

        }
    }
}