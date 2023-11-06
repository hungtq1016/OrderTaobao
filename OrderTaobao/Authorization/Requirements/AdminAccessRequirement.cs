using Microsoft.AspNetCore.Authorization;

namespace BaseSource.BackendAPI.Authorization.Requirements
{
    public class AdminAccessRequirement : IAuthorizationRequirement
    {
        public string UserId { get; set; }
        public AdminAccessRequirement()
        {
          
        }
    }
}
