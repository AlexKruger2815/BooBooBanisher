using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using Npgsql;
using System.Data;
using bbb.Models;
using bbb.DAO;

namespace bbb.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");
    private UserDAO dao = new UserDAO();
    [HttpPost("newuser")]
    public IActionResult newUser([FromBody] UserModel model)
    {
        System.Console.WriteLine($"UserModel value: {model} = {model.userID}, {model.username} "); 
        try
        {
            dao.postUser(model);
        }
        catch (Exception ex)
        {
            // Handle any exceptions 
            return BadRequest("Error: " + ex.Message);
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
