using Microsoft.AspNetCore.Mvc;

namespace bbb.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("newuser")]
    public IActionResult newUser([FromBody] UserModel model){
        System.Console.WriteLine($"UserModel value: {model} = {model.userID}, {model.username} ");
        return Ok($"UserModel value: {model} = {model.userID}, {model.username} ");
    } 
}

public class UserModel {
    public string username { get; set; }
    public int userID { get; set; }
}