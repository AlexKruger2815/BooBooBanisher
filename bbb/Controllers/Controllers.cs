using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using Npgsql;
using System.Data;


namespace bbb.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");
    private DAO dao = new DAO();
    [HttpPost("newuser")]
    public IActionResult newUser([FromBody] UserModel model)
    {
        string sql = "INSERT into public.users(username) VALUES (@Username)";
        System.Console.WriteLine($"UserModel value: {model} = {model.userID}, {model.username} ");
        System.Console.WriteLine(db + " with " + sql);
        using (IDbConnection connection = new NpgsqlConnection(db))
        {
            try
            {
                // Open the connection
                connection.Open();
                var cmd = new NpgsqlCommand(sql, (NpgsqlConnection?)connection);
                cmd.Parameters.AddWithValue("username", model.username);
                System.Console.WriteLine(cmd.CommandText);
                var response = cmd.ExecuteNonQuery();
                Console.WriteLine($"{response} response.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions 
                return BadRequest("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        // user received well, add to db
        return Ok($"UserModel value: {model} = {model.userID}, {model.username} ");
    }

    [HttpGet(Name = "getUser")]
    public IActionResult getUser(string username)
    {
        {
            string sql = $"select * from public.users where username = \'" + username + "\'";
            System.Console.WriteLine(db + " with " + sql);
            try
            {
                var resp = dao.getUser("where username = \'" + username + "\'");
                return Ok(resp);
            }
            catch (Exception ex)
            {
                // Handle any exceptions 
                return BadRequest("Error: " + ex.Message);
            }
        }
    }
}

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private DAO dao = new DAO();
    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");
    [HttpGet("")]
    public IActionResult getAllMessage(int status)
    {

        return Ok($"Get Msg: {status}");
    }

    [HttpGet("all")]
    public IActionResult getAllMessage()
    {
        try
        {
            var resp = dao.getMessage();
            return Ok(resp);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    [HttpGet("")]
    public IActionResult getCategories()
    {
        return Ok($"all categories");
    }
}

[ApiController]
[Route("[controller]")]
public class SessionController : ControllerBase
{
    [HttpPost("")]
    public IActionResult newSession([FromBody] SessionModel model)
    {
        DateTime currentDateTime = DateTime.Now;
        return Ok($"new session model: {model.messageID} {model.userID} {model.sessionID} => {currentDateTime}");
    }
}

public class DAO
{
    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");
    public IEnumerable<MessageModel> getMessage(string filter = "")
    {
        string sql = $"select * from public.messages" + filter;
        System.Console.WriteLine($"msg DAO: {filter}");
        using (IDbConnection connection = new NpgsqlConnection(db))
        {
            // Open the connection
            connection.Open();
            var response = connection.Query<MessageModel>(sql);
            System.Console.WriteLine(response);
            return response;
        }
    }
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
public class UserModel
{
    public UserModel()
    {
    }
    public UserModel(string name, int id)
    {
        username = name;
        userID = id;
    }
    public string username { get; set; }
    public int userID { get; set; }
}

public class MessageModel
{
    public int messageID { get; }
    public int categoryID { get; set; }
    public string content { get; set; }
}

public class CategoryModel
{
    public int categoryID { get; }
    public string categoryType { get; set; }
}

public class SessionModel
{
    public int sessionID { get; set; }
    public int userID { get; set; }
    public int messageID { get; set; }
}