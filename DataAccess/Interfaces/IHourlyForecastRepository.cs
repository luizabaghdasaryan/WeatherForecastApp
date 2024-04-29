using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IHourlyForecastRepository : IBaseRepository<HourlyForecast>
    {
        IQueryable<HourlyForecast> GetAllWithDetails();
    }
}