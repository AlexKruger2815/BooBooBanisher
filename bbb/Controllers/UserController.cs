using Microsoft.AspNetCore.Mvc;
using bbb.Models;
using bbb.DAO;
using System.Text.Json;
using bbb.Helpers;
namespace bbb.Controllers;

[ApiController]
[Route("[controller]")]
//localhost/User/
public class UserController : ControllerBase
{
    private UserDAO dao = new UserDAO();
    [HttpPost("newUser")]
    //localhost/user/newuser
    public IActionResult newUser([FromBody] UserModel model)
    {
        if (!Helper.CheckToken(HttpContext.Request.Headers))
        {
            return BadRequest("Invalid Token");
        }
        System.Console.WriteLine($"UserModel value: {model} = {model.userID}, {model.username} ");
        if (model.username is not string || model.username == null || model.username.Equals("#"))
        {
            return BadRequest("Invalid Username");
        }
        else
        {
            try
            {
                dao.postUser(model);
            }
            catch (Exception ex)
            {
                // Handle any exceptions 
                return BadRequest("Error: " + ex.Message);
            }
        }
        // user received well, add to db
        return Ok($"UserModel value: {model} = {model.userID}, {model.username} ");
    }

    [HttpGet("getUser")]
    public IActionResult getUser(string username)
    {
        if (!Helper.CheckToken(HttpContext.Request.Headers))
        {
            return BadRequest("Invalid Token");
        }
        else if (username == null || username.Equals("#"))
        {
            return BadRequest("Invalid Username");
        }
        string sql = $"select * from public.users where username = \'" + username + "\'";
        System.Console.WriteLine(" with " + sql);
        try
        {
            var resp = dao.getUser("where username = \'" + username + "\'").First();
            return Ok(resp);
        }
        catch (Exception ex)
        {
            // Handle any exceptions 
            return BadRequest("Error: " + ex.Message);
        }
    }

    [HttpGet("")]
    public IActionResult getNewUser()
    {
        try
        {
            string token = "";
            HttpContext.Request.Headers.TryGetValue("Authorization", out var temp);
            token = temp!;
            var response = getUsername(token);
            JsonDocument el = JsonDocument.Parse(response.Result!);
            var username = el.RootElement.GetProperty("login").GetString();
            System.Console.WriteLine("username Valeu " + username);
            try
            {
                var user = new UserModel(username!);
                int ans = dao.postUser(user);
                if (ans == -1)
                {
                    return BadRequest("Illegal Values");
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("duplicate key value violates unique constraint \"unique_username\""))
                {
                    return Ok(dao.getUser(" where username = \'" + username + "\'"));
                    // return Forbid()
                }
                else
                    return BadRequest(e.Message);
            }
            //receive the token from frontend, get username directly from github
            UserModel returnUser =dao.getUser(" where username = \'" + username + "\'").First(); 
            return Ok(returnUser);
        }
        catch (Exception ex) { return BadRequest($"Bad token request: {ex.Message}"); }
    }
    private async Task<string?> getUsername(string token)
    {
        HttpClient client = new HttpClient();
        // client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1521.3 Safari/537.36");
        var response = await client.GetAsync("https://api.github.com/user");
        var resp = await response.Content.ReadAsStringAsync();
        return resp;
    }
}
