using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IDailyForecastRepository : IBaseRepository<DailyForecast>
    {
        Task<IEnumerable<DailyForecast>> GetAllWithDetailsAsync();

        Task<IEnumerable<DailyForecast>> GetByRegionWithDetailsAsync(int regionId);

        Task<DailyForecast?> GetByIdWithDetailsAsync(int id);

        Task<DailyForecast?> GetByDateAsync(DateTime date, int regionId);

        IEnumerable<DailyForecast> GetByPeriod(int regionId, DateTime startDate, DateTime endDate);
    }
}