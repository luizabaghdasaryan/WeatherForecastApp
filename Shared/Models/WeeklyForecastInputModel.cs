using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class WeeklyForecastInputModel
    {
        [Required(ErrorMessage = "Field is required")]
        public int RegionId { get; set; }

        public List<DailyForecastForWeeklyInputModel> DailyForecasts { get; set; } = null!;
    }
}