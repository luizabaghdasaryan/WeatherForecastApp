using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("HourlyForecast")]
    public class HourlyForecast : BaseEntity
    {
        [Column("ForecastId")]
        [Required]
        [ForeignKey("Forecast")]
        public int ForecastId { get; set; }

        [Column("Hour")]
        [Required]
        [Range(0, 23)]
        public int Hour { get; set; }

        [Column("TemperatureC")]
        [Required]
        [Range(-20, 45)]
        public int TemperatureC { get; set; }

        public DailyForecast Forecast { get; set; } = null!;
    }
}