using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public enum TerrainType
    {
        Mountains,
        Foothills,
        Valleys
    }

    public class RegionModel
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Column("TerrainType")]
        public TerrainType? TerrainType { get; set; }
    }
}

