using bbb.Models;
using Npgsql;
using System.Data;

namespace bbb.DAO;

public class UserDAO
{

    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");
    public IEnumerable<UserModel> getUser(string filter = "")
    {
        string sql = "select * from public.users " + filter;
        System.Console.WriteLine("user dao: " + sql);
        using (NpgsqlConnection connection = new NpgsqlConnection(db))
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                // Open the connection
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                List<UserModel> users = new List<UserModel>();
                while (reader.Read())
                {
                    var model = new UserModel();
                    model.userID = reader.GetInt32(0);
                    model.username = reader.GetString(1);
                    System.Console.WriteLine(model);
                    users.Add(model);
                }
                reader.Close();
                System.Console.WriteLine(users.Count);
                return users;
            }
        }
    }
    public int postUser(UserModel model)
    {
        if (model.username is not string || model.username == null || model.username == "#")
        {
            return -1;
        }
        string sql = "INSERT into public.users(username) VALUES (@Username)";
        using (IDbConnection connection = new NpgsqlConnection(db))
        {
            // Open the connection
            connection.Open();
            var cmd = new NpgsqlCommand(sql, (NpgsqlConnection?)connection);
            cmd.Parameters.AddWithValue("username", model.username);
            System.Console.WriteLine(cmd.CommandText);
            var response = cmd.ExecuteNonQuery();
            Console.WriteLine($"{response} rows affected.");
            connection.Close();
            return response;
        }

    }

}