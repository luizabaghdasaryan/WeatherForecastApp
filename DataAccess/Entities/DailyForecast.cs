using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace DataAccess.Entities
{
    [Table("DailyForecast")]
    public class DailyForecast : BaseEntity
    {
        [Column("Date")]
        [Required]
        public DateTime Date { get; set; }

        [Column("MinTemperature")]
        [Required]
        public int MinTemperature { get; set; }

        [Column("MaxTemperature")]
        [Required]
        public int MaxTemperature { get; set; }

        [Column("SummaryId")]
        [Required]
        [ForeignKey("Summary")]
        public int SummaryId { get; set; }

        [Column("RegionId")]
        [Required]
        [ForeignKey("Region")]
        public int RegionId { get; set; }

        public Region Region { get; set; } = null!;
        public Summary Summary { get; set; } = null!;
        public ICollection<HourlyForecast> HourlyForecasts { get; set; } = new Collection<HourlyForecast>();
    }
}