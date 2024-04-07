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
    SessionDAO dao = new SessionDAO();
    [HttpPost("")]
    public IActionResult newSession([FromBody] SessionModel model)
    {
        if (model.messageID <= 0 || model.userID <= 0)
        {
            return BadRequest("Invalid SessionModel Entity");
        }
        DateTime currentDateTime = DateTime.Now;
        try
        {
            var resp = dao.insertSessions(model, currentDateTime);
            return Ok(resp + " rows affected");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok($"new session model: {model.messageID} {model.userID} {model.sessionID} => {currentDateTime}");
    }
}
