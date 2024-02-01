using Core;

namespace OAuth2Service.DTOs
{
    public class GroupRequest : EntityRequest
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
