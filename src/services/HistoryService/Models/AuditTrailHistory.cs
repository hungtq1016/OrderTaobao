using Core;

namespace HistoryService.Models
{
    public class AuditTrailHistory : Entity
    {
        public required string TableName { get; set; }

        public string DeviceIP { get; set; }

        public required string Action { get; set; }

        public string? EntityId { get; set; }

        public int? StatusCode { get; set; }

        public string Value { get; set; }
    }
}
