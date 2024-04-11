using bbb.Models;
using Npgsql;
using System.Data;

namespace bbb.DAO;
public class SessionDAO
{
    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");
    public int insertSessions(SessionModel model, DateTime time)
    {
        string sql = "INSERT into public.sessions(userid, messageid, createdAt) VALUES (@userID, @messageID, @time)";
        using (IDbConnection connection = new NpgsqlConnection(db))
        {
            // Open the connection
            connection.Open();
            var cmd = new NpgsqlCommand(sql, (NpgsqlConnection?)connection);
            System.Console.WriteLine("isnert session "+model );
            cmd.Parameters.AddWithValue("userID", model.userID);
            cmd.Parameters.AddWithValue("messageID", model.messageID);
            cmd.Parameters.AddWithValue(parameterName: "time", time);
            System.Console.WriteLine(cmd.CommandText);
            var response = cmd.ExecuteNonQuery();
            Console.WriteLine($"{response} rows affected.");
            connection.Close();
            return response;
        }
    }
    public IEnumerable<SessionModel> getSessions(string filter)
    {
        string sql = "select * from public.sessions " + filter; System.Console.WriteLine("user dao: " + sql);
        using (NpgsqlConnection connection = new NpgsqlConnection(db))
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                // Open the connection
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                List<SessionModel> sessions = new List<SessionModel>();
                while (reader.Read())
                {
                    var model = new SessionModel();
                    model.createdAt = reader.GetDateTime("createdAt");
                    System.Console.WriteLine(model);
                    sessions.Add(model);
                }
                reader.Close();
                System.Console.WriteLine(sessions.Count);
                return sessions;
            }
        }
    }
}
