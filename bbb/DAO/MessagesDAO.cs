using bbb.Models;
using Npgsql;
using System.Data;

namespace bbb.DAO;
public class MessageDAO
{
    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");
    public IEnumerable<MessageModel> getMessage(string filter = "")
    {
        string sql = $"select messageid as messageID, messagecategoryid as categoryid, messagecontent as content from public.messages " + filter;
        System.Console.WriteLine($"msg DAO: {sql}");
        using (NpgsqlConnection connection = new NpgsqlConnection(db))
        {
            using (var command = new NpgsqlCommand(sql, connection))
            {
                // Open the connection
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                List<MessageModel> messages = new List<MessageModel>();
                while (reader.Read())
                {
                    var model = new MessageModel();
                    model.categoryID = reader.GetInt32(1);
                    model.content = reader.GetString(2);
                    System.Console.WriteLine("dao value : "+model);
                    messages.Add(model);
                }
                reader.Close();
                System.Console.WriteLine("size "+messages.Count());
                return messages;
            }
        }
    }
}
