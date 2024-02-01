
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("WARDS")]
    public class Ward : BaseAddress
    {

        [Column("DISTRICT_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("District")]
        public string? DistrictId { get; set; }
        public District District { get; set; } = null!;
        public ICollection<Address>? Address { get; } = new List<Address>();

    }
}
