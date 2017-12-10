using System;
using System.Globalization;
using Neo4jClient;

namespace GraphDb
{
    public class DbOperations
    {
        private IGraphClient _graphClient;
        public IGraphClient Connect(string url, string login, string password)
        {
             _graphClient = new GraphClient(new Uri(url),login,password);
            try
            {
                _graphClient.Connect();
                Console.WriteLine("Connected");
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return _graphClient;
        }


        public void AddPerson(string name, string surname, string dateOfBirth, string speciality,string group)
        {
            try
            {
                Random rand = new Random();
                var newPerson = new Person
                {
                    ID = rand.Next(1, 999),
                    Name = name,
                    Surname = surname,
                    DateOfBirth = DateTime.ParseExact(dateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Speciality = speciality,
                    Group = group
                };

                _graphClient.Cypher
                    .Create("(person:Person{newPerson})")
                    .WithParam("newPerson", newPerson)
                    .ExecuteWithoutResults();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void RelatePeople(int id1,int id2)
        {
            try
            {
                _graphClient.Cypher.Match("(p1:Person),(p2:Person)")
                    .Where((Person p1) => p1.ID == id1)
                    .AndWhere((Person p2) => p2.ID == id2)
                    .Create("(p1)-[:Friends]->(p2)")
                    .ExecuteWithoutResults();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
