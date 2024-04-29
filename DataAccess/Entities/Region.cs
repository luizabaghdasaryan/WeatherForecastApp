using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public enum TerrainType
    {
        Mountains,
        Foothills,
        Valleys
    }

    [Table("Region")]
    public class Region : BaseEntity
    {
        [Column("Name")]
        [Required]
        public string? Name { get; set; }

        [Column("TerrainType")]
        public TerrainType? TerrainType { get; set; }
    }
}