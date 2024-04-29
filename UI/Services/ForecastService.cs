using Shared.Models;
using System.Net.Http.Json;

namespace UI.Services
{
    public class ForecastService : IForecastService
    {
        private readonly HttpClient _httpClient;

        public ForecastService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DailyForecastModel[]?> GetDailyForecastsByRegion(int regionId)
        {
            return await _httpClient.GetFromJsonAsync<DailyForecastModel[]>($"api/forecasts/daily/{regionId}");
        }

        public async Task<DailyForecastDetailsModel?> GetHourlyDetailsAsync(int regionId, string date)
        {
            return await _httpClient.GetFromJsonAsync<DailyForecastDetailsModel>($"api/forecasts/hourly/{regionId}?date={date}");
        }

        public async Task<RegionModel[]?> GetRegionAsync()
        {
            return await _httpClient.GetFromJsonAsync<RegionModel[]>($"api/regions");
        }

        public async Task DeleteForecastAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/forecasts/{id}");
        }

        public async Task<SummaryModel[]?> GetSummariesAsync()
        {
            return await _httpClient.GetFromJsonAsync<SummaryModel[]>($"api/summaries");
        }

        public async Task<int> CreateDailyForecastAsync(DailyForecastInputModel forecast)
        {
            var response = await _httpClient.PostAsJsonAsync("api/forecasts/daily", forecast);
            response.EnsureSuccessStatusCode();
            var newForecast = await response.Content.ReadFromJsonAsync<DailyForecastModel>();

            return newForecast!.Id;
        }

        public async Task CreateWeeklyForecastAsync(WeeklyForecastInputModel forecast)
        {
            await _httpClient.PostAsJsonAsync("api/forecasts/weekly", forecast);
        }

        public async Task<DateTime?> GetUpcomingWarmerDay(int regionId, string date)
        {
            return await _httpClient.GetFromJsonAsync<DateTime>($"api/forecasts/upcoming-warmer-day/" +
                                                      $"{regionId}?date={date}");
        }

        public Task<string[]?> GetAvailableDays(int regionId)
        {
            return _httpClient.GetFromJsonAsync<string[]>($"api/forecasts/available-weather-days/{regionId}");
        }

        public Task<DailyForecastDetailsModel?> GetById(int id)
        {
            return _httpClient.GetFromJsonAsync<DailyForecastDetailsModel>($"api/forecasts/{id}");
        }

        public async Task UpdateForecastAsync(int id, DailyForecastUpdateModel forecast)
        {
            await _httpClient.PutAsJsonAsync($"api/forecasts/{id}", forecast);
        }
    }
}