using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    internal sealed class SummaryRepository : RepositoryBase<Summary>, ISummaryRepository
    {
        public SummaryRepository(WeatherForecastDbContext context)
            : base(context) 
        {
        }
    }
}