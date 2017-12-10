using System;

namespace GraphDb
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DbOperations operations = new DbOperations();
            operations.Connect("http://localhost:7474/db/data","","");

 
            int choice,id1,id2,id;
            string name, surname, dateOfBirth, speciality, group, description;
            do
            {
                Console.WriteLine("Welcome to first shabby implementation of GraphDB in C#. Below are some basic operation which You can perform.");
                Console.WriteLine("1. Add person.");
                Console.WriteLine("2. Relate 2 people as friends.");
                Console.WriteLine("3. Show people.");
                Console.WriteLine("4. Show friends for specific person.");
                Console.WriteLine("5. Edit name of person.");
                Console.WriteLine("6. Create group.");
                Console.WriteLine("7. Show groups.");
                Console.WriteLine("8. Add person to group.");
                Console.WriteLine("9. Show people in group.");
                Console.WriteLine("10. Add post to group.");
                Console.WriteLine("11. Show posts in group. ");
                Console.WriteLine("12. Delete person");
                Console.WriteLine("13. Exit");
                Console.WriteLine("Choose operation");

                int.TryParse(Console.ReadLine(),out choice);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter name");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter surname");
                        surname = Console.ReadLine();
                        Console.WriteLine("Enter date of birth in format yyyy-MM-dd");
                        dateOfBirth = Console.ReadLine();
                        Console.WriteLine("Enter speciality");
                        speciality = Console.ReadLine();
                        Console.WriteLine("Enter group");
                        group = Console.ReadLine();
                        operations.AddPerson(name,surname,dateOfBirth,speciality,group);
                        break;
                    case 2:
                        Console.WriteLine("Enter ID of first person");
                        int.TryParse(Console.ReadLine(), out id1);
                        Console.WriteLine("Enter ID of second person");
                        int.TryParse(Console.ReadLine(), out id2);
                        operations.RelatePeople(id1,id2);
                        break;
                    case 3:
                        operations.ShowAllPeople();
                        break;
                    case 4:
                        Console.WriteLine("Enter ID of person for which You want to find friends");
                        int.TryParse(Console.ReadLine(), out id);
                        operations.ShowFriends(id);
                        break;
                    case 5:
                        Console.WriteLine("Enter ID of person for which You want to edit name");
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine("Enter new name");
                        name = Console.ReadLine();
                        operations.UpdateName(id,name);
                        break;
                    case 6:
                        Console.WriteLine("Enter name of group");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter description of group");
                        description = Console.ReadLine();
                        operations.AddGroup(name,description);
                        break;
                    case 7:
                        operations.ShowAllGroups();
                        break;
                    case 8:
                        Console.WriteLine("Enter ID of group");
                        int.TryParse(Console.ReadLine(), out id1);
                        Console.WriteLine("Enter ID of  person");
                        int.TryParse(Console.ReadLine(), out id2);
                        operations.AddPersonToGroup(id1, id2);
                        break;
                    case 9:
                        Console.WriteLine("Enter ID of group for which You want to find friends");
                        int.TryParse(Console.ReadLine(), out id);
                        operations.ShowGroupMember(id);
                        break;
                    case 10:
                        Console.WriteLine("Enter ID of group");
                        int.TryParse(Console.ReadLine(), out id1);
                        Console.WriteLine("Enter ID of  person");
                        int.TryParse(Console.ReadLine(), out id2);
                        Console.WriteLine("Enter topic");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter content");
                        description = Console.ReadLine();
                        operations.AddPostToGroup(id1, id2,name,description);
                        break;
                    case 11:
                        Console.WriteLine("Enter ID of group for which You want to find friends");
                        int.TryParse(Console.ReadLine(), out id);
                        operations.ShowPostsInGroup(id);
                        break;
                    case 12:
                        Console.WriteLine("Enter ID of person  which You want to delete");
                        int.TryParse(Console.ReadLine(), out id);
                        operations.DeleteUser(id);
                        break;
                    default:
                        break;
                }
         
            } while (choice != 13);

        }
    }
}