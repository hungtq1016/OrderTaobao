using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BaseSource.BackendAPI.Authorization
{
    public class AllowGuestAttribute : TypeFilterAttribute
    {
        public AllowGuestAttribute() : base(typeof(AllowGuestFilter)){}
    }

    public class AllowGuestFilter : IAuthorizationFilter
    {
        public AllowGuestFilter(){}

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.Result = new OkResult();
            return ;
        }
    }
}
