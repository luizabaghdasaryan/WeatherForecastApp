using AutoMapper;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Shared.Models;

namespace BusinessLogic.Services
{
    public class ForecastService : IForecastService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public ForecastService(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DailyForecastDetailsModel>> GetAllAsync()
        {
            var forecasts = await repository.DailyForecastRepository.GetAllWithDetailsAsync();
            var forecastModels = mapper.Map<IEnumerable<DailyForecastDetailsModel>>(forecasts);

            return forecastModels;
        }

        public async Task<DailyForecastDetailsModel> GetByIdAsync(int id)
        {
            var forecast = await repository.DailyForecastRepository.GetByIdWithDetailsAsync(id)
                ?? throw new ForecastNotFoundException(id);

            var forecastModel = mapper.Map<DailyForecastDetailsModel>(forecast);

            return forecastModel;
        }
        public async Task<IEnumerable<DailyForecastModel>> GetDailyForecastByRegionAsync(int regionId)
        {
            var region = await repository.RegionRepository.GetByIdAsync(regionId)
                ?? throw new RegionNotFoundException(regionId);
            var forecasts = await repository.DailyForecastRepository.GetByRegionWithDetailsAsync(regionId);
            var forecastModels = mapper.Map<IEnumerable<DailyForecastModel>>(forecasts);

            return forecastModels;
        }
        public async Task<DateTime> GetUpcomingWarmerDay(DateTime date, int regionId)
        {
            var dayDetails = await repository.DailyForecastRepository
                .GetByDateAsync(date, regionId)
                ?? throw new ForecastNotFoundException(date, regionId);

            var dayAverage = dayDetails.HourlyForecasts.Average(hf => hf.TemperatureC);

            var forecastDetails = repository.HourlyForecastRepository.GetAllWithDetails();

            var upcomingForecastDetails = forecastDetails
                .Where(f => f.Forecast.Date > date && f.Forecast.RegionId == regionId);

            var groupedResult = upcomingForecastDetails
                .GroupBy(fd => new { fd.Forecast.Date, fd.Forecast.RegionId })
                .Select(group => new
                {
                    group.Key.Date,
                    Average = group.Average(item => item.TemperatureC)
                });

            var nearWarmerDay = groupedResult.FirstOrDefault(g => g.Average > dayAverage)?.Date;

            return nearWarmerDay ?? date;
        }

        public IEnumerable<string> GetAvailableDays(int regionId)
        {
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = startDate.AddDays(6);
            var upcomingDays = Enumerable.Range(0, 7).Select(index => startDate.AddDays(index))
                .ToList();

            var addedDays = repository.DailyForecastRepository.GetByPeriod(regionId, startDate, endDate)
                .Select(x => x.Date);
            var availableDays = upcomingDays.Except(addedDays)
                .Select(x => x.ToString("dd-MM-yyyy")).ToList();

            return availableDays;
        }

        public async Task<DailyForecastModel> AddDailyForecastAsync(DailyForecastInputModel model)
        {
            var dailyForecast = mapper.Map<DailyForecast>(model);
            UpdateTemperatures(dailyForecast);
            await repository.DailyForecastRepository.AddAsync(dailyForecast);
            await repository.SaveAsync();
            var newForecastModel = mapper.Map<DailyForecastModel>(dailyForecast);

            return newForecastModel;
        }

        public async Task<IEnumerable<DailyForecastModel>> AddWeeklyForecastAsync(WeeklyForecastInputModel model)
        {
            var dailyForecasts = mapper.Map<IEnumerable<DailyForecast>>(model.DailyForecasts);
            foreach (var forecast in dailyForecasts)
            {
                forecast.RegionId = model.RegionId;
                UpdateTemperatures(forecast);
            }

            repository.DailyForecastRepository.AddRange(dailyForecasts);
            await repository.SaveAsync();
            var newForecastModels = mapper.Map<IEnumerable<DailyForecastModel>>(dailyForecasts);

            return newForecastModels;
        }

        public async Task UpdateAsync(int id, DailyForecastUpdateModel model)
        {
            var forecast = await repository.DailyForecastRepository.GetByIdWithDetailsAsync(id)
                ?? throw new ForecastNotFoundException(id);

            mapper.Map(model, forecast);
            foreach (var _forecast in model.HourlyForecasts)
            {
                var hourlyForecast = forecast.HourlyForecasts.Single(f => f.Hour == _forecast.Hour);
                mapper.Map(_forecast, hourlyForecast);
            }

            if (model.HourlyForecasts.Any())
            {
                UpdateTemperatures(forecast);
            }

            repository.DailyForecastRepository.Update(forecast);
            await repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var forecast = await repository.DailyForecastRepository.GetByIdAsync(id)
                ?? throw new ForecastNotFoundException(id);

            repository.DailyForecastRepository.Delete(forecast);
            await repository.SaveAsync();
        }

        private static void UpdateTemperatures(DailyForecast forecast)
        {
            var temperatures = forecast.HourlyForecasts
                .Select(hf => hf.TemperatureC);
            forecast.MinTemperature = temperatures.Min();
            forecast.MaxTemperature = temperatures.Max();
        }

        public async Task GenerateAndAddWeatherData(int days, int hoursInterval, int regionId)
        {
            var regionsIds = await repository.RegionRepository.GetByIdAsync(regionId)
                ?? throw new RegionNotFoundException(regionId);
            var summariesIds = repository.SummaryRepository.GetAll().Select(s => s.Id).ToArray();
            var today = DateTime.Now.Date;
            for (int i = 0; i < days; ++i)
            {
                var forecast = new DailyForecast
                {
                    Date = today.AddDays(i),
                    RegionId = regionId,
                    SummaryId = summariesIds[Random.Shared.Next(summariesIds.Length)]
                };

                var list = new List<HourlyForecast>();
                for (int hour = 0; hour < 24; hour += hoursInterval)
                {
                    var hourlyForecast = new HourlyForecast
                    {
                        Hour = hour,
                        TemperatureC = Random.Shared.Next(-20, 45),
                    };

                    forecast.HourlyForecasts.Add(hourlyForecast);
                }

                forecast.MinTemperature = forecast.HourlyForecasts.Select(a => a.TemperatureC).Min();
                forecast.MaxTemperature = forecast.HourlyForecasts.Select(a => a.TemperatureC).Max();
                await repository.DailyForecastRepository.AddAsync(forecast);
                await repository.SaveAsync();
            }
        }
    }
}