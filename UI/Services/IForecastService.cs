using Shared.Models;

namespace UI.Services
{
    public interface IForecastService
    {
        Task<DailyForecastModel[]?> GetDailyForecastsByRegion(int regionId);
        Task<DailyForecastDetailsModel?> GetHourlyDetailsAsync(int regionId, string date);
        Task<DailyForecastDetailsModel?> GetById(int id);
        Task<string[]?> GetAvailableDays(int regionId);
        Task<RegionModel[]?> GetRegionAsync();
        Task<SummaryModel[]?> GetSummariesAsync();
        Task DeleteForecastAsync(int id);
        Task<int> CreateDailyForecastAsync(DailyForecastInputModel forecast);
        Task CreateWeeklyForecastAsync(WeeklyForecastInputModel forecast);
        Task UpdateForecastAsync(int id, DailyForecastUpdateModel forecast);
        Task<DateTime?> GetUpcomingWarmerDay(int regionId, string date);
    }
}