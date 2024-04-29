namespace DataAccess.Interfaces
{
    public interface IRepositoryManager
    {
        IDailyForecastRepository DailyForecastRepository { get; }

        IHourlyForecastRepository HourlyForecastRepository { get; }

        IRegionRepository RegionRepository { get; }

        ISummaryRepository SummaryRepository { get; }

        Task SaveAsync();
    }
}