using BusinessLogic.Interfaces;
using BusinessLogic.Validations;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastsController : ControllerBase
    {
        private readonly IServiceManager service;

        public ForecastsController(IServiceManager service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyForecastDetailsModel>>> GetHourlyForecasts()
        {
            var forecasts = await service.ForecastService.GetAllAsync();

            return Ok(forecasts);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DailyForecastModel>> GetById(int id)
        {
            var forecast = await service.ForecastService.GetByIdAsync(id);

            return Ok(forecast);
        }

        [HttpGet("daily/{regionId:int}")]
        public async Task<ActionResult<RegionModel>> GetDailyByRegion([FromRoute] int regionId)
        {
            var forecasts = await service.ForecastService.GetDailyForecastByRegionAsync(regionId);

            return Ok(forecasts);
        }

        [HttpGet("upcoming-warmer-day/{regionId:int}")]
        public async Task<ActionResult<DateTime>> Get([FromRoute] int regionId, [FromQuery] [DateValidation] DateTime date)
        {
            var warmerDay = await service.ForecastService.GetUpcomingWarmerDay(date, regionId);

            return Ok(warmerDay);
        }

        [HttpGet("available-weather-days/{regionId:int}")]
        public ActionResult<string> GetAvailableDays([FromRoute] int regionId)
        {
            var days = service.ForecastService.GetAvailableDays(regionId);

            return Ok(days);
        }

        [HttpPost("daily")]
        public async Task<ActionResult> PostDailyForecast([FromBody] [DailyForecastCreationValidation] DailyForecastInputModel forecast)
        {
            var createdForecast = await service.ForecastService.AddDailyForecastAsync(forecast);

            return CreatedAtAction(nameof(GetById), new { id = createdForecast.Id }, createdForecast);

        }

        [HttpPost("weekly")]
        public async Task<ActionResult> PostWeeklyForecast([FromBody] [WeeklyForecastCreationValidation] WeeklyForecastInputModel forecasts)
        {
            var createdForecast = await service.ForecastService.AddWeeklyForecastAsync(forecasts);

            return Created("", createdForecast);
        }

        [HttpPost("generate/{regionId:int}")]
        public async Task<ActionResult> GenerateForecast([FromRoute] int regionId, [FromQuery] int days, [FromQuery] int hoursInterval)
        {
            await service.ForecastService.GenerateAndAddWeatherData(days, hoursInterval, regionId);

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] DailyForecastUpdateModel model)
        {
            await service.ForecastService.UpdateAsync(id, model);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await service.ForecastService.DeleteAsync(id);

            return NoContent();
        }
    }
}