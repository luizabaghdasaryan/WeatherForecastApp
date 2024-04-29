using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class DailyForecastUpdateModel
    {
        [Required(ErrorMessage = "Field is required")]
        public int SummaryId { get; set; }

        [Required]
        public List<HourlyForecastInputModel> HourlyForecasts { get; set; } = null!;
    }
}