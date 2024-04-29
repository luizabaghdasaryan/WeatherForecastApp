using DataAccess.Data;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly WeatherForecastDbContext context;
        private readonly Lazy<IDailyForecastRepository> dailyForecastRepository;
        private readonly Lazy<IHourlyForecastRepository> hourlyForecastRepository;
        private readonly Lazy<IRegionRepository> regionRepository;
        private readonly Lazy<ISummaryRepository> summaryRepository;

        public RepositoryManager(WeatherForecastDbContext context)
        {
            this.context = context;
            dailyForecastRepository = new Lazy<IDailyForecastRepository>(() => new
            DailyForecastRepository(context));
            hourlyForecastRepository = new Lazy<IHourlyForecastRepository>(() => new
            HourlyForecastRepository(context));
            regionRepository = new Lazy<IRegionRepository>(() => new
            RegionRepository(context));
            summaryRepository = new Lazy<ISummaryRepository>(() => new
            SummaryRepository(context));
        }

        public IDailyForecastRepository DailyForecastRepository => dailyForecastRepository.Value;

        public IHourlyForecastRepository HourlyForecastRepository => hourlyForecastRepository.Value;

        public IRegionRepository RegionRepository => regionRepository.Value;

        public ISummaryRepository SummaryRepository => summaryRepository.Value;

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}