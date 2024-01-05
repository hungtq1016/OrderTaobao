
namespace BaseSource.Model
{
    public class AuditTrailHistory : BaseEntity
    {
        public string UserId { get; set; }
        public required string TableName { get; set; }
        public required string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
