using bbb.Models;
using Npgsql;
using Microsoft.AspNetCore.Mvc;
using Dapper;
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
        using (IDbConnection connection = new NpgsqlConnection(db))
        {
            // Open the connection
            connection.Open();
            var response = connection.Query<UserModel>(sql);
            System.Console.WriteLine(response);
            return response;
        }
        return null;
    }
}