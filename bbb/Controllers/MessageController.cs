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
        var resp = dao.getMessage();
        return Ok(resp);
    }

    [HttpGet("all")]
    public IActionResult getAllMessage()
    {
        try
        {
            var resp = dao.getMessage("where messagecategoryid in (1,3)");
            var respList = resp.ToList();
            MessageModel model = respList[new Random().Next(respList.Count)];
            return Ok(model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}