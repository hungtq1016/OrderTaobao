
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    public class AdministrativeUnit : BaseEntity
    {
        [Column("NAME", TypeName = "nvarchar"), MaxLength(36)]
        public string Name { get; set; } = string.Empty;

        [Column("SLUG", TypeName = "nvarchar"), MaxLength(36)]
        public string Slug { get; set; } = string.Empty;
        public ICollection<Ward> Wards { get; } = new List<Ward>();
        public ICollection<District> Districts { get; } = new List<District>();
        public ICollection<Province> Provinces { get; } = new List<Province>();
    }
}
