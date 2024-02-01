using Core;

namespace OAuth2Service.DTOs
{
    public class AssignmentRequest : EntityRequest
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
