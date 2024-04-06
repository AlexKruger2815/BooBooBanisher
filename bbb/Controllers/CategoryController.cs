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
public class CategoryController : ControllerBase
{
    [HttpGet("")]
    public IActionResult getCategories()
    {
        return Ok($"all categories");
    }
}