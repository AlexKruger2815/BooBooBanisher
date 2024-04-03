using Microsoft.AspNetCore.Mvc;

namespace bbb.Controllers;

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
        return Ok(new UserModel(username,id));
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