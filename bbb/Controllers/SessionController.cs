using Microsoft.AspNetCore.Mvc;
using bbb.Models;
using bbb.DAO;
using bbb.Helpers;
namespace bbb.Controllers;



[ApiController]
[Route("[controller]")]
public class SessionController : ControllerBase
{
    SessionDAO dao = new SessionDAO();
    [HttpPost("")]
    public IActionResult newSession([FromBody] SessionModel model)
    {
        if (!Helper.CheckToken(HttpContext.Request.Headers))
        {
            return BadRequest("Invalid Token");
        }
        else if (model.messageID <= 0)
        {
            return BadRequest("Invalid MessageID");
        }
        else if (model.userID <= 0)
        {
            return BadRequest("Invalid UserID");
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
    }

    //GET http://localhost:8080/session parameter (userID)
    [HttpGet("")]
    public IActionResult getAllSessions(int userID)
    {
        string query = "where userid = " + userID;

        try
        {
            return Ok(dao.getSessions(query).ToList());
        }
        catch (System.Exception)
        {
            throw;
        }

    }
    // /session/stats
    [HttpGet("stats")]
    public IActionResult getStats(int userID, DateTime? start = null, DateTime? end = null)
    {
        DateTime _start = start ?? DateTime.MinValue;
        DateTime _end = end ?? DateTime.Now;
        StatsDAO statsDAO = new StatsDAO();
        String dateSql = " AND s.createdat >= \'" + _start + "\' AND s.createdat <= \'" + _end + "\'";
        System.Console.WriteLine(dateSql);
        var result = statsDAO.getStats(" where  s.userid = " + userID + dateSql);
        //retrieve all details
        return Ok(result);
    }
}
