using Core;

namespace OAuth2Service.DTOs
{
    public class RoleRequest : EntityRequest
    {  
        public string Name { get; set; }
        public string Note { get; set; }
    }
}
