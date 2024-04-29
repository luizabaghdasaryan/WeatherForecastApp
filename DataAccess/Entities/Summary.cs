using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Summary")]
    public class Summary : BaseEntity
    {
        [Column("Name")]
        [Required]
        public string? Name { get; set; }
    }
}