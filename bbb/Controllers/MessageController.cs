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
public class MessageController : ControllerBase
{
    private MessageDAO dao = new MessageDAO();
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