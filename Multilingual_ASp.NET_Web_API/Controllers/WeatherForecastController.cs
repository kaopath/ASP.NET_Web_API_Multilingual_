using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Multilingual_ASp.NET_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Request.Query.TryGetValue("locale", out StringValues locale))
            {
                if (!string.IsNullOrWhiteSpace(locale.ToString()) &&
                    !(string.Compare(Thread.CurrentThread.CurrentCulture?.Name, locale.ToString(), true) == 0))
                {
                    CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo(locale.ToString());
                    CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(locale.ToString());
                }
            }

            base.OnActionExecuting(context);
        }


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            string[] Summaries = new[]
      {
           WeatherLocals.Freezing,
            WeatherLocals.Bracing,
            WeatherLocals.Chilly,
            WeatherLocals.Cool,
            WeatherLocals.Mild,
            WeatherLocals.Warm,
            WeatherLocals.Balmy,
            WeatherLocals.Hot,
            WeatherLocals.Sweltering,
            WeatherLocals.Scorching
        };
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Summary = Summaries[rng.Next(Summaries.Length)],
                TemperatureC = rng.Next(-20, 55)

            })
            .ToArray();
        }
    }
}
