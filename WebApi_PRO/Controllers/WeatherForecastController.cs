using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_PRO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<WeatherForecast> _forecasts;
        private static int _currentIndex;

        static WeatherForecastController()
        {
            _forecasts = new List<WeatherForecast>();
            var rng = new Random();
            _forecasts.AddRange(
            Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = index,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = string.Empty
            }));

            _currentIndex = 6;
        }

        public WeatherForecastController()
        {
        }

        [HttpPost]
        public IActionResult Post(WeatherForecast weatherForecast)
        {
            weatherForecast.Id = _currentIndex++;
            _forecasts.Add(weatherForecast);

            return Created(weatherForecast.Id.ToString(), weatherForecast);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var forecast = _forecasts.Find(x => x.Id == id);
            if(forecast != null)
            {
                _forecasts.Remove(forecast);
                return Ok(forecast);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, WeatherForecast weatherForecast)
        {
            var forecast = _forecasts.Find(x => x.Id == id);
            if (forecast != null)
            {
                _forecasts.Remove(forecast);
                weatherForecast.Id = id;
                _forecasts.Add(weatherForecast);

                return Ok(forecast);
            }

            return NotFound();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _forecasts;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var forecast = _forecasts.Find(x => x.Id == id);
            return forecast != null ? Ok(forecast) : NotFound();
        }
    }
}
