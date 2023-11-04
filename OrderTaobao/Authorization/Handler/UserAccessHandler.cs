using BaseSource.BackendAPI.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.BackendAPI.Authorization.Handler
{
    public class UserAccessHandler : AuthorizationHandler<UserAccessRequirement>
    {
        private readonly UserManager<User> _userManager;

        public UserAccessHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAccessRequirement requirement)
        {
           
            return Task.CompletedTask;
        }
    }
}
