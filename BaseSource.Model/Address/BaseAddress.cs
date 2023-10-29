using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    public abstract class BaseAddress : BaseEntity
    {
        [Column("NAME", TypeName = "nvarchar"), MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("SLUG", TypeName = "nvarchar"), MaxLength(100)]
        public string Slug { get; set; } = string.Empty;

        [Column("NAME_EN", TypeName = "nvarchar"), MaxLength(100)]
        public string NameEn { get; set; } = string.Empty;

        [Column("EN_SLUG", TypeName = "nvarchar"), MaxLength(100)]
        public string EnSlug { get; set; } = string.Empty;

        [Column("CODE")]
        public UInt16 CODE { get; set; }

        [Column("ADMINISTRATIVE_UNIT_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("AdministrativeUnit")]
        public string AdministrativeUnitID { get; set; }
        public AdministrativeUnit AdministrativeUnit { get; set; }
    }
}
