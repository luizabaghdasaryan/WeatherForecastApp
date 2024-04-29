using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    internal sealed class DailyForecastRepository : RepositoryBase<DailyForecast>, IDailyForecastRepository
    {
        public DailyForecastRepository(WeatherForecastDbContext context) : base(context) { }

        public async Task<IEnumerable<DailyForecast>> GetAllWithDetailsAsync()
        {
            return await Context.Set<DailyForecast>()
                .Include(df => df.Region)
                .Include(df => df.Summary)
                .Include(df => df.HourlyForecasts)
                .ToListAsync();
        }

        public async Task<DailyForecast?> GetByIdWithDetailsAsync(int id)
        {
            return await Context.Set<DailyForecast>()
                .Include(df => df.Region)
                .Include(df => df.Summary)
                .Include(df => df.HourlyForecasts)
                .SingleOrDefaultAsync(df => df.Id == id);
        }

        public async Task<IEnumerable<DailyForecast>> GetByRegionWithDetailsAsync(int regionId)
        {
            return await Context.Set<DailyForecast>()
                .Include(df => df.Region)
                .Include(df => df.Summary)
                .Include(df => df.HourlyForecasts)
                .Where(df => df.RegionId == regionId)
                .ToListAsync();
        }

        public async Task<DailyForecast?> GetByDateAsync(DateTime date, int regionId) =>
             await Context.Set<DailyForecast>()
                .Include(df => df.Region)
                .Include(df => df.Summary)
                .Include(df => df.HourlyForecasts)
                .SingleOrDefaultAsync(df => df.Date == date && df.RegionId == regionId);

        public IEnumerable<DailyForecast> GetByPeriod(int regionId, DateTime startDate, DateTime endDate) =>
            GetAll().Where(df => df.RegionId == regionId &&
            df.Date >= startDate && df.Date <= endDate)
            .ToArray();
    }
}