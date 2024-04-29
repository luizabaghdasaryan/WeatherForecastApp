using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    internal sealed class HourlyForecastRepository : RepositoryBase<HourlyForecast>, IHourlyForecastRepository
    {
        public HourlyForecastRepository(WeatherForecastDbContext context) 
            : base(context) 
        {
        }

        public IQueryable<HourlyForecast> GetAllWithDetails()
        {
            return Context.Set<HourlyForecast>()
                    .Include(hf => hf.Forecast)
                        .ThenInclude(df => df.Region)
                    .Include(hf => hf.Forecast)
                        .ThenInclude(df => df.Summary);
        }
    }
}