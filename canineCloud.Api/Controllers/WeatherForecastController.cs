using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace canineCloud.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private static readonly string MANU_DOGS = "You're not authorized to do this!";

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [Authorize("manipulate:dogs")]
    public ActionResult Get()
    {
        return Ok(new {
        Forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray(), 
        Message = MANU_DOGS});
    }
}
