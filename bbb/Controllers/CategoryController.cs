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
        if (model.categoryType is not string || model.categoryType == null || model.categoryID  <= 0)
        {
            return BadRequest("Invalid entity inputs");
        }
        else
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
}