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
public class SessionController : ControllerBase
{
    [HttpPost("")]
    public IActionResult newSession([FromBody] SessionModel model)
    {
        DateTime currentDateTime = DateTime.Now;
        return Ok($"new session model: {model.messageID} {model.userID} {model.sessionID} => {currentDateTime}");
    }
}
