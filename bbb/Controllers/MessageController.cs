using Microsoft.AspNetCore.Mvc;
using bbb.Models;
using bbb.DAO;
using bbb.Helpers;

namespace bbb.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private MessageDAO dao = new MessageDAO();
    readonly string? db = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build().GetConnectionString("DefaultConnection");
    [HttpGet("all")]
    public IActionResult getAllMessage(int status)
    {
        if (!Helper.CheckToken(HttpContext.Request.Headers))
        {
            return BadRequest("Invalid Token");
        }
        var resp = dao.getMessage();
        return Ok(resp);
    }

    [HttpGet("error")]
    public IActionResult getErrorMessage(int userID)
    {
        if (!Helper.CheckToken(HttpContext.Request.Headers))
        {
            return BadRequest("Invalid Token");
        }
        try
        {
            //give you a random message
            // wasnt sure how Im going to know if its an error or a success
            //create a session
            var resp = dao.getMessage(" where messagecategoryid in (1)"); //receiving an error
            var respList = resp.ToList();
            System.Console.WriteLine(respList[0].ToString());
            MessageModel model = respList[new Random().Next(respList.Count)];
            createSession(userID, model.messageID);
            System.Console.WriteLine($"new model {model.messageID}, {model.categoryID}, {model.content}");
            return Ok(model.content);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("success")]
    public IActionResult getSuccessfulMessage(int userID)
    {
        if (!Helper.CheckToken(HttpContext.Request.Headers))
        {
            return BadRequest("Invalid Token");
        }
        try
        {
            //give you a random message
            // wasnt sure how Im going to know if its an error or a success
            // also create a session
            var resp = dao.getMessage("where messagecategoryid in (2)"); //receiving an error
            var respList = resp.ToList();
            MessageModel model = respList[new Random().Next(respList.Count)];
            createSession(userID, model.messageID);
            return Ok(model.content);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    public IActionResult getOtherMessage(int userID)
    {
        if (!Helper.CheckToken(HttpContext.Request.Headers))
        {
            return BadRequest("Invalid Token");
        }
        var resp = dao.getMessage("where messagecategoryid in (3)"); //receiving an error
        var respList = resp.ToList();
        MessageModel model = respList[new Random().Next(respList.Count)];
        createSession(userID, model.messageID);
        return null;
    }
    private void createSession(int userID, int messageID)
    {
        SessionDAO session = new SessionDAO();
        SessionModel newSession = new SessionModel();
        newSession.userID = userID;
        newSession.messageID = messageID;
        session.insertSessions(newSession, DateTime.Now);
    }
}