using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSource.Model
{
    public class ImageUser : BaseEntity
    {
        [Column("USER_ID", TypeName = "nvarchar"), MaxLength(450)]
        public string UserId { get; set; }

        [Column("IMAGE_ID", TypeName = "varchar"), MaxLength(36)]
        public string ImageId { get; set; }
        public User User { get; set; } = null!;
        public Image Image { get; set; } = null!;
    }
}
