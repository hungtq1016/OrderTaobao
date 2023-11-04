using Microsoft.AspNetCore.Authorization;

namespace BaseSource.BackendAPI.Authorization.Requirements
{
    public class UserAccessRequirement : IAuthorizationRequirement
    {
        public string UserId { get; set; }
        public UserAccessRequirement(string userId)
        {
            UserId = userId;
        }
    }
}
