using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace bbb.Controllers;

private static readonly string connectionString = Configuration.GetConnectionString("DefaultConnection");
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("newuser")]
    public IActionResult newUser([FromBody] UserModel model)
    {
        System.Console.WriteLine(Url.Link("getUser", null));
        System.Console.WriteLine($"UserModel value: {model} = {model.userID}, {model.username} ");
        // user received well, add to db
        return Ok($"UserModel value: {model} = {model.userID}, {model.username} ");
    }

    [HttpGet(Name = "getUser")]
    public IActionResult getUser(string username)
    {
        System.Console.WriteLine("getuser: " + username);
        int id = -1;
        return Ok(new UserModel(username, id));
    }
}

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    [HttpGet("")]
    public IActionResult getAllMessage(int status)
    {
        return Ok($"Get Msg: {status}");
    }

    [HttpGet("all")]
    public IActionResult getAllMessage()
    {
        return Ok($"return all messages");
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
    public int sessionID { get; set;}
    public int userID { get; set; }
    public int messageID { get; set; } 
}