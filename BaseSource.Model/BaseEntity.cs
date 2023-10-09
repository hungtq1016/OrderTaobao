using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    public abstract class BaseEntity
    {
        [Column("CREATED_AT")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("CREATED_BY")]
        public string CreatedBy { get; set; } = string.Empty;

        [Column("UPDATED_AT")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Column("UPDATED_BY")]
        public string UpdatedBy { get; set; } = string.Empty;

    }
}
