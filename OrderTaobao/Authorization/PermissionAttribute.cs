using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace BaseSource.BackendAPI.Authorization
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;

        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var hasClaim = context.HttpContext.User.Claims
                        .Any(claim =>
                            (claim.Type == _claim.Type && claim.Value == _claim.Value) //Role have permission
                            || (claim.Type == "Admin" && claim.Value == "All") //Admin role
                        );

            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }

}
