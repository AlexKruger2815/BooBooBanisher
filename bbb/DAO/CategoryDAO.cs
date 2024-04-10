using System.Data;
using bbb.Models;
using Npgsql;

namespace bbb.DAO;

public class CategoryDAO
{

    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");
    public IEnumerable<CategoryModel> GetCategories(string filter = "")
    {
        string sql = "select * from public.messagecategory " + filter;
        System.Console.WriteLine("category dao: " + sql);

        using (NpgsqlConnection connection = new NpgsqlConnection(db))
        {
            using (var command = new NpgsqlCommand(sql, connection))
            {
                // Open the connection
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                List<CategoryModel> categories = new List<CategoryModel>();
                while (reader.Read())
                {
                    CategoryModel model = new CategoryModel();
                    model.categoryType = reader.GetString(1);
                    categories.Add(model);
                    System.Console.WriteLine(model);
                }
                reader.Close();
                System.Console.WriteLine(categories.Count);
                return categories;
            }
        }
        return null;
    }
    public int post(CategoryModel model)
    {
        string sql = "INSERT into public.messagecategory(messagecategoryid, messagecategorytype) VALUES (@id, @type)";
        using (IDbConnection connection = new NpgsqlConnection(db))
        {
            // Open the connection
            connection.Open();
            var cmd = new NpgsqlCommand(sql, (NpgsqlConnection?)connection);
            cmd.Parameters.AddWithValue("type", model.categoryType);
            cmd.Parameters.AddWithValue("id", model.categoryID);
            System.Console.WriteLine(cmd.CommandText);
            var response = cmd.ExecuteNonQuery();
            Console.WriteLine($"{response} rows affected.");
            connection.Close();
            return response;
        }

    }
}