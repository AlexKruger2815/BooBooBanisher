using System.Data;
using bbb.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Npgsql;

namespace bbb.DAO;

public class CategoryDAO
{

    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");
    public IEnumerable<CategoryModel> getUser(string filter = "")
    {
        string sql = "select * from public.messagecategory " + filter;
        System.Console.WriteLine("category dao: " + sql);
        using (IDbConnection connection = new NpgsqlConnection(db))
        {
            // Open the connection
            connection.Open();
            var response = connection.Query<CategoryModel>(sql);
            System.Console.WriteLine(response);
            return response;
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