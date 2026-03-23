using Microsoft.AspNetCore.Mvc;

namespace Weather_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        ServiceWeather serviceWeather;
        public WeatherController(ServiceWeather serviceWeather)
        {
            this.serviceWeather = serviceWeather;
        }
        //[HttpGet]
        //public async Task<IActionResult> Get(string city)
        //{
        //    try
        //    {
        //        ServiceWeather serviceWeather = new ServiceWeather();
        //        string weatherData = await serviceWeather.GetWeather(city);
        //        return Ok(weatherData);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet("weathers")]
        public async Task<IActionResult> GetWeathers(string city)
        {
            try
            {
                List<Weather>? weatherData = await serviceWeather.GetWeathers(city);
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
