using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using Npgsql;
using System.Data;
using bbb.Models;
using bbb.DAO;
using Microsoft.VisualBasic;

namespace bbb.Controllers;


[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    public CategoryDAO dao = new CategoryDAO();
    [HttpGet("")]
    public IActionResult getCategories()
    {
        return Ok($"all categories");
    }
    
    [HttpPost("")]
    public IActionResult insertCategory([FromBody] CategoryModel model)
    {
        try
        {
            var resp = dao.post(model);
            return Ok(resp + " rows affected");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}