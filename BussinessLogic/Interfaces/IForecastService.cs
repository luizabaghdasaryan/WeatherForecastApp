using Shared.Models;

namespace BusinessLogic.Interfaces
{
    public interface IForecastService
    {
        Task<IEnumerable<DailyForecastDetailsModel>> GetAllAsync();

        Task<DailyForecastDetailsModel> GetByIdAsync(int id);

        Task<IEnumerable<DailyForecastModel>> GetDailyForecastByRegionAsync(int regionId);

        Task<DailyForecastModel> AddDailyForecastAsync(DailyForecastInputModel model);

        Task<IEnumerable<DailyForecastModel>> AddWeeklyForecastAsync(WeeklyForecastInputModel model);

        Task<DateTime> GetUpcomingWarmerDay(DateTime date, int regionId);

        Task UpdateAsync(int id, DailyForecastUpdateModel model);

        Task DeleteAsync(int id);

        IEnumerable<string> GetAvailableDays(int regionId);
        
        Task GenerateAndAddWeatherData(int days, int hoursInterval, int regionId);
    }
}