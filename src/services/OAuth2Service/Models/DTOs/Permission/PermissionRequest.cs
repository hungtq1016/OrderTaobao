using Core;

namespace OAuth2Service.DTOs
{
    public class PermissionRequest : EntityRequest
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
