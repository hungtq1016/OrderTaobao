using System.Security.Claims;
using BaseSource.BackendAPI.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.BackendAPI.Authorization.Handler
{
    public class AdminAccessHandler : AuthorizationHandler<AdminAccessRequirement>
    {
        private readonly UserManager<User> _userManager;

        public AdminAccessHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAccessRequirement requirement)
        {
           string userId = context.User
                .Claims.First(claim => claim.Type == ClaimTypes.Name)
                .Value;
            Console.WriteLine(userId);
            return Task.CompletedTask;
        }
    }
}
