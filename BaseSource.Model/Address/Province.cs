
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("PROVINCES")]
    public class Province : BaseAddress
    {
        public ICollection<District> Districts { get; } = new List<District>();

    }
}
