using Microsoft.AspNetCore.Mvc;

namespace Weather_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(string city)
        {
            try
            {
                ServiceWeather serviceWeather = new ServiceWeather();
                string weatherData = await serviceWeather.GetWeather(city);
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
