using bbb.Models;
using Npgsql;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;

namespace bbb.DAO;
public class MessageDAO
{
    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");
    public IEnumerable<MessageModel> getMessage(string filter = "")
    {
        string sql = $"select messageid, messagecategoryid as categoryid, messagecontent as content from public.messages " + filter;
        System.Console.WriteLine($"msg DAO: {sql}");
        using (IDbConnection connection = new NpgsqlConnection(db))
        {
            // Open the connection
            connection.Open();
            var response = connection.Query<MessageModel>(sql);
            System.Console.WriteLine(response);
            return response;
        }
    }
}
