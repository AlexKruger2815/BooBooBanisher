using System.Data;
using bbb.Models;
using Npgsql;

namespace bbb.DAO;

public class StatsDAO
{
    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");

    public IEnumerable<StatsModel> getStats(string filter)
    {
        string sql = "select c.messagecategorytype as category, m.messagecontent as content, s.createdAt from public.sessions s inner join messages m on m.messageid = s.messageid inner join messagecategory c on c.messagecategoryid = m.messagecategoryid " + filter;
        System.Console.WriteLine("hi : " + sql);
        using (NpgsqlConnection connection = new NpgsqlConnection(db))
        {
            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                // command.CommandType = System.Data.CommandType.StoredProcedure;
                // Open the connection
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                List<StatsModel> models = new List<StatsModel>();
                while (reader.Read())
                {
                    var model = new StatsModel
                    {
                        category = reader.GetString("category"),
                        content = reader.GetString("content"),
                        createdAt = reader.GetDateTime("createdAt")
                    };
                    System.Console.WriteLine(model);
                    models.Add(model);
                }
                reader.Close();
                System.Console.WriteLine(models.Count);
                return models;
            }
        }
    }
}