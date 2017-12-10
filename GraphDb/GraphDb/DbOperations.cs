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
            _graphClient = new GraphClient(new Uri(url), login, password);
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


        public void AddPerson(string name, string surname, string dateOfBirth, string speciality, string group)
        {
            try
            {
                var rand = new Random();
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

        public void RelatePeople(int id1, int id2)
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

        public void ShowAllPeople()
        {
            try
            {
                var users = _graphClient.Cypher
                    .Match("(person:Person)")
                    .Return(person => person.As<Person>())
                    .Results;

                foreach (var zm in users)
                {
                    Console.WriteLine("ID: {0}", zm.ID);
                    Console.WriteLine("Name: {0}", zm.Name);
                    Console.WriteLine("Surname: {0}", zm.Surname);
                    Console.WriteLine("Date of birth: {0}", zm.DateOfBirth);
                    Console.WriteLine("Speciality: {0}", zm.Speciality);
                    Console.WriteLine("Group: {0}", zm.Group);
                    Console.WriteLine("---------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ShowFriends(int id)
        {
            try
            {
                var users = _graphClient.Cypher
                    .OptionalMatch("(person:Person)-[FRIENDS]-(friend:Person)")
                    .Where((Person person) => person.ID == id)
                    .Return(friend => friend.As<Person>())
                    .Results;
                foreach (var zm in users)
                {
                    Console.WriteLine("ID: {0}", zm.ID);
                    Console.WriteLine("Name: {0}", zm.Name);
                    Console.WriteLine("Surname: {0}", zm.Surname);
                    Console.WriteLine("Date of birth: {0}", zm.DateOfBirth);
                    Console.WriteLine("Speciality: {0}", zm.Speciality);
                    Console.WriteLine("Group: {0}", zm.Group);
                    Console.WriteLine("---------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void UpdateName(int id, string name)
        {
            try
            {
                _graphClient.Cypher
                    .Match("(person:Person)")
                    .Where((Person person) => person.ID == id)
                    .Set("person.Name = {name} ")
                    .WithParam("name", name)
                    .ExecuteWithoutResults();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void AddGroup(string name, string description)
        {
            try
            {
                var rand = new Random();
                var newGroup = new Group
                {
                    ID = rand.Next(1, 999),
                    Name = name,
                    Description = description
                };

                _graphClient.Cypher
                    .Create("(group:Group{newGroup})")
                    .WithParam("newGroup", newGroup)
                    .ExecuteWithoutResults();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ShowAllGroups()
        {
            try
            {
                var users = _graphClient.Cypher
                    .Match("(group:Group)")
                    .Return(group => group.As<Group>())
                    .Results;

                foreach (var zm in users)
                {
                    Console.WriteLine("ID: {0}", zm.ID);
                    Console.WriteLine("Name: {0}", zm.Name);
                    Console.WriteLine("Description: {0}", zm.Description);
                    Console.WriteLine("---------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void AddPersonToGroup(int groupId, int personId)
        {
            try
            {
                _graphClient.Cypher.Match("(g1:Group),(p1:Person)")
                    .Where((Group g1) => g1.ID == groupId)
                    .AndWhere((Person p1) => p1.ID == personId)
                    .Create("(p1)-[:Joined]->(g1)")
                    .ExecuteWithoutResults();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ShowGroupMember(int id)
        {
            try
            {
                var users = _graphClient.Cypher
                    .OptionalMatch("(person:Person)-[Joined]-(group:Group)")
                    .Where((Group group) => group.ID == id)
                    .Return(person => person.As<Person>())
                    .Results;
                foreach (var zm in users)
                {
                    Console.WriteLine("ID: {0}", zm.ID);
                    Console.WriteLine("Name: {0}", zm.Name);
                    Console.WriteLine("Surname: {0}", zm.Surname);
                    Console.WriteLine("Date of birth: {0}", zm.DateOfBirth);
                    Console.WriteLine("Speciality: {0}", zm.Speciality);
                    Console.WriteLine("Group: {0}", zm.Group);
                    Console.WriteLine("---------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void AddPostToGroup(int groupId, int personId, string topic, string content)
        {
            try
            {
                var rand = new Random();
                var newPost = new Post
                {
                    ID = rand.Next(1, 999),
                    Topic = topic,
                    Content = content
                };

                _graphClient.Cypher
                    .Match("(group:Group)")
                    .Where((Group group) => group.ID == groupId)
                    .Match("(person:Person)")
                    .Where((Person person) => person.ID == personId)
                    .Create("(post:Post{newPost})")
                    .WithParam("newPost", newPost)
                    .Create("(post)-[:Published]->(group)")
                    .Create("(person)-[:Created]->(post)")
                    .ExecuteWithoutResults();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ShowPostsInGroup(int id)
        {
            try
            {
                var users = _graphClient.Cypher
                    .OptionalMatch("(post:Post)-[Published]-(group:Group)")
                    .Where((Group group) => group.ID == id)
                    .Return(post => post.As<Post>())
                    .Results;
                foreach (var zm in users)
                {
                    Console.WriteLine("ID: {0}", zm.ID);
                    Console.WriteLine("Topic: {0}", zm.Topic);
                    Console.WriteLine("Content: {0}", zm.Content);
                    Console.WriteLine("---------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                _graphClient.Cypher
                    .OptionalMatch("(person:Person)<-[r]-()")
                    .Where((Person person) => person.ID == id)
                    .Delete("r, person")
                    .ExecuteWithoutResults();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}