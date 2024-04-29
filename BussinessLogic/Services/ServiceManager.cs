using AutoMapper;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IForecastService> forecastService;
        private readonly Lazy<IRegionService> regionService;
        private readonly Lazy<ISummaryService> summaryService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            forecastService = new Lazy<IForecastService>(() => new
            ForecastService(repositoryManager, mapper));
            regionService = new Lazy<IRegionService>(() => new
            RegionService(repositoryManager, mapper));
            summaryService = new Lazy<ISummaryService>(() => new
            SummaryService(repositoryManager, mapper));

        }
        public IForecastService ForecastService => forecastService.Value;

        public IRegionService RegionService => regionService.Value;

        public ISummaryService SummaryService => summaryService.Value;
    }
}
