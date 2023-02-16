using Microsoft.Data.SqlClient;

namespace LinqExperiment
{
    public class User
    {
        public User(int id, string? firstName, string? lastName, string? email, string? gender, string? iPAddress)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Gender = gender;
            IPAddress = iPAddress;
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? IPAddress { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> userList = new List<User>();

            string connectionString = @"data source = .\SQLEXPRESS; initial catalog=MOCK_DATA; Encrypt = false; Trusted_Connection=true;";

            string commandString = "SELECT * FROM MOCK_DATA";

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand(commandString,connection);

            SqlDataReader reader = command.ExecuteReader();

            foreach(var item in reader)
            {
                userList.Add(new User(int.Parse(
                    reader.GetValue(0).ToString()),
                    reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(), 
                    reader.GetValue(3).ToString(), 
                    reader.GetValue(4).ToString(), 
                    reader.GetValue(5).ToString()));
            }

            //var result = userList.OrderBy(user => user.FirstName).ThenByDescending(user => user.LastName);

            //foreach(var item in result)
            //{
            //    Console.Write(item.Id + " | ");
            //    Console.Write(item.FirstName + " | ");
            //    Console.Write(item.LastName + " | ");
            //    Console.Write(item.Gender + " | ");
            //    Console.Write(item.Email + " | ");
            //    Console.WriteLine(item.IPAddress);
            //}

            //var result = userList.Select(user => user.FirstName).Where(user => user.StartsWith("A")).Count();

            //Console.WriteLine(result);


            // METHOD SYNTAX
            var result = userList.Where(x => x.Gender == "Male").Select(x => x.IPAddress).ToList();

            // QUERY SYNTAX
            var result2 = from user in userList where user.Gender == "Male" select user.IPAddress;

            
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("-------------------------");

            foreach (var item in result2)
            {
                Console.WriteLine(item);
            }
        }
    }
}