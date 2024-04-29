using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    internal sealed class RegionRepository : RepositoryBase<Region>, IRegionRepository
    {
        public RegionRepository(WeatherForecastDbContext context) 
            : base(context) 
        { 
        }
    }
}