namespace BusinessLogic.Interfaces
{
    public interface IServiceManager
    {
        IForecastService ForecastService { get; }

        IRegionService RegionService { get; }

        ISummaryService SummaryService { get; }
    }
}