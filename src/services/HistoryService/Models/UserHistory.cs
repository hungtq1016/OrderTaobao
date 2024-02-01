using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("CUSTOMER_HISTORY")]
    public class UserHistory
    {
        public UserHistory()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public string Id { get; set; }

        [Column("OLD_VALUE", TypeName = "nvarchar")]
        public string OldValue { get; set; } = string.Empty;

        [Column("NEW_VALUE", TypeName = "nvarchar")]
        public string NewValue { get; set; } = string.Empty;

        [Column("FIELD", TypeName = "nvarchar"), MaxLength(36)]
        public string Field { get; set; } = string.Empty;

        [Column("MODIFIED_AT")]
        public DateTime ModifiedAt { get; set; }

        [Column("USER_ID", TypeName = "nvarchar"), MaxLength(450)]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public User? User { get; set; } = null!;

    }
}
