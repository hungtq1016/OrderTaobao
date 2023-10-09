
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("ADMIN")]
    public class Admin : BaseUser
    {
        [Column("ENABLE")]
        public bool Enable { get; set; } = true;
    }
}
