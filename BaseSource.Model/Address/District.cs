
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("DISTRICTS")]
    public class District : BaseAddress
    {
        [Column("PROVINCE_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Province")]
        public Guid ProvinceId { get; set; }
        public Province Province { get; set; } = null!;
        public ICollection<Ward> Wards { get; } = new List<Ward>();
    }
}
