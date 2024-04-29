using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class DailyForecastForWeeklyInputModel
    {
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int SummaryId { get; set; }

        [Required]
        public List<HourlyForecastInputModel> HourlyForecasts { get; set; } = null!;
    }
}