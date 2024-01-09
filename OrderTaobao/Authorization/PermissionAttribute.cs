using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Security.Claims;

namespace BaseSource.BackendAPI.Authorization
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute() : base(typeof(PermissionFilter))
        {
            Arguments = new string[] {};
        }
    }

    public class PermissionFilter : IAsyncAuthorizationFilter
    {

        public PermissionFilter()
        {
        }
     
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;

            if (!context.HttpContext.User.Identity!.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var roleManager = context.HttpContext.RequestServices.GetRequiredService<RoleManager<Role>>();

            request.RouteValues.TryGetValue("controller", out var controllerValue);
            var controllerName = (string)(controllerValue ?? string.Empty);
            var method = request.Method;

            var permission = $"{controllerName}.{method}";

            var userId = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            if (! await HavePermission(userManager,roleManager,user,permission))
            {
                context.Result = new ForbidResult();
                return;
            }

        }

        private async Task<bool> HavePermission(UserManager<User> userManager, RoleManager<Role> roleManager, User user, string permission)
        {
            IList<string> userRoles = await userManager.GetRolesAsync(user);
            IList<Claim> userClaims = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>(userClaims);

            var roleClaimsTasks = userRoles.Select(async userRole =>
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    var roleClaims = await roleManager.GetClaimsAsync(role);
                    claims.AddRange(roleClaims);
                }
            });

            await Task.WhenAll(roleClaimsTasks);

            var hasClaim = claims.Any(claim =>
                (claim.Type == "permission" && claim.Value == permission) || // Role has permission
                (claim.Type == "Admin" && claim.Value == "All") // Admin role
            );

            return hasClaim;
        }

    }

}
