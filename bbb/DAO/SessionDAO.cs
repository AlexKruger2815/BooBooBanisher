using bbb.Models;
using Npgsql;
using Microsoft.AspNetCore.Mvc;
using Dapper;
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
}