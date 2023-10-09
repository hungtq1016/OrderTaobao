
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    [Table("PROVINCES")]
    public class Province: BaseAddress
    {
        public ICollection<District> Districts { get; } = new List<District>();

    }
}
