namespace OAuth2Service.Filters
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(string? type, string? value) : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { new Claim(type, value) };
        }
    }

    public class PermissionFilter : IAuthorizationFilter
    {
        private readonly ILogger<PermissionFilter> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly Claim? _claim;

        public PermissionFilter(Claim claim, ILogger<PermissionFilter> logger, IMemoryCache memoryCache)
        {
            _claim = claim;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null || context.HttpContext == null) return;

            var userId = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = context.HttpContext.User;
            if (context?.HttpContext?.User?.Claims == null)
    {
        _logger.LogError("HttpContext, User, or Claims is null");
        context.Result = new UnauthorizedResult();
        return;
    }
            if (!IsAuthenticated(context, userId)) return;

            var permission = $"{context.HttpContext.Request.RouteValues["controller"]}.{context.HttpContext.Request.Method}";

            if (!HasPermission(user,userId,permission))
            {
                _logger.LogInformation($"{userId} try to connect ${permission} but failed!");
                context.Result = new ForbidResult();
            }
        }

        private bool IsAuthenticated(AuthorizationFilterContext context, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new UnauthorizedResult();
                return false;
            }
            return true;
        }

        private bool HasPermission(ClaimsPrincipal principal, string userId, string permission)
        {

            if (!_memoryCache.TryGetValue(userId, out List<Permission> permissions))
            {
                _memoryCache.Set(userId, permissions);
            }

            return CheckPermission(principal,permission);
        }

        private bool CheckPermission(ClaimsPrincipal principal, string permission)
        {
            var claimsValue = principal.Claims;
            string permissionLower = permission.ToLower();

            return principal.Claims.Any(p =>
                (p.Type.ToLower() == "controller" && p.Value.ToLower() == permissionLower) ||
                (p.Type.ToLower() == _claim.Type?.ToLower() && p.Value.ToLower() == _claim.Value?.ToLower()) ||
                (p.Type.ToLower() == "admin" && p.Value.ToLower() == "all"));
        }
    }
}
