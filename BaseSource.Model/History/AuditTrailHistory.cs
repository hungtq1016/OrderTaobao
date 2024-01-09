
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("AUDIT_TRAIL")]
    public class AuditTrailHistory : BaseEntity
    {
        [Column("USER_ID", TypeName = "nvarchar"), MaxLength(450)]
        [ForeignKey("User")]
        public string? UserId { get; set; }

        public User? User { get; set; }

        [Column("TABLE_NAME", TypeName = "nvarchar"), MaxLength(100)]
        public required string TableName { get; set; }

        [Column("DEVICE_IP", TypeName = "varchar"), MaxLength(50)]
        public string DeviceIP { get; set; }

        [Column("ACTION", TypeName = "varchar"), MaxLength(10)]
        public required string Action { get; set; }

        [Column("ENTITY_ID", TypeName = "varchar"), MaxLength(36)]
        public string? EntityId { get; set; }

        [Column("STATUS_CODE")]
        public int? StatusCode { get; set; }

        [Column("VALUE", TypeName = "text")]
        public string Value { get; set; }
    }
}
