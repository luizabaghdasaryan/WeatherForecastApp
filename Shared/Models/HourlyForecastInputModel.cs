using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class HourlyForecastInputModel
    {
        [Required(ErrorMessage = "Field is required")]
        [Range(0, 23)]
        public int Hour { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Range(-20, 40)]
        public int TemperatureC { get; set; }
    }
}