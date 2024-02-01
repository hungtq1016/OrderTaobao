using Core;

namespace OAuth2Service.DTOs
{
    public class PermissionResponse : Entity
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
