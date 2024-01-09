using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    public abstract class BaseEntity
    {

        [Key]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public string Id { get; set; } = string.Empty;

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        [Column("UPDATED_AT")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        [Column("ENABLE")]
        public bool Enable { get; set; } = true;

    }
}
