using Microsoft.AspNetCore.Mvc;

namespace bbb.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }


    [HttpPost("HelloPost")]
    public IActionResult hello()
    {
        return Ok("Hello PostIt World");
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        System.Console.WriteLine("HI \n");
        System.Console.WriteLine(Url.Link("HelloPost", null));
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

[ApiController]
[Route("login/[controller]")]
public class OtherController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello from Login");
    }
}
