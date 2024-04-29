using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    [Table("Forecast")]
    public class Forecast : BaseEntity
    {
        [Column("Date")]
        [Required]
        public DateTime Date { get; set; }

        [Column("TemperatureC")]
        [Required]
        [Range(-20, 45)]
        public int TemperatureC { get; set; }

        [Column("SummaryId")]
        [Required]
        [ForeignKey("Summary")]
        public int SummaryId { get; set; }

        [Column("RegionId")]
        [Required]
        [ForeignKey("Region")]
        public int RegionId { get; set; }

        

        public Summary Summary { get; set; } = null!;
        public Region Region { get; set; } = null!;
    }
}