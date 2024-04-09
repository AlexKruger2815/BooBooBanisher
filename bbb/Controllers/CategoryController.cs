using Microsoft.AspNetCore.Mvc;
using bbb.Models;
using bbb.DAO;
using bbb.Helpers;

namespace bbb.Controllers;


[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    public CategoryDAO dao = new CategoryDAO();
    [HttpGet("")]
    public IActionResult getCategories()
    {
        if (!Helper.CheckToken(HttpContext.Request.Headers))
        {
            return BadRequest("Invalid Token");
        }
        dao.GetCategories();
        return Ok($"all categories");
    }

    [HttpPost("")]
    public IActionResult insertCategory([FromBody] CategoryModel model)
    {
        if (!Helper.CheckToken(HttpContext.Request.Headers))
        {
            return BadRequest("Invalid Token");
        }
        if (model.categoryType is not string || model.categoryType == null || model.categoryID <= 0)
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