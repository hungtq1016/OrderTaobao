
using System.Security.Claims;
using BaseSource.Dto;
using BaseSource.Helper;
using BaseSource.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.BackendAPI.Services
{
    public interface IRoleService
    {
        Task<Response<PageResponse<List<Role>>>> GetPagedData(PaginationRequest request, string route);

        Task<Response<RoleResponse>> GetClaimByRole(string id);

        Task<Response<bool>> AddClaim(IdentityRoleClaim<string> claim);
    }

    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IUriService _uriService;

        public RoleService(RoleManager<Role> roleManager, IUriService uriService)
        {
            _roleManager = roleManager;
            _uriService = uriService;
        }

        public async Task<Response<PageResponse<List<Role>>>> GetPagedData(PaginationRequest request, string route)
        {
            if (_roleManager.Roles is null)
                return ResponseHelper
                        .CreateErrorResponse<PageResponse<List<Role>>>
                        (500, "The server cannot process the request for an unknown reason");

            PaginationRequest validFilter = new PaginationRequest(request.PageNumber, request.PageSize);

            int totalRecords = await _roleManager.Roles.CountAsync();

            if (totalRecords is 0)
                return ResponseHelper.CreateErrorResponse<PageResponse<List<Role>>>
                        (404, "No roles found.");

            List<Role> roles = await _roleManager.Roles
                .Where(role => role.Name != "Super Admin")
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize).ToListAsync();

            PageResponse<List<Role>> pagedResponse = PaginationHelper.CreatePagedReponse(roles, validFilter, Convert.ToUInt16(totalRecords), _uriService, route);

            return ResponseHelper.CreateSuccessResponse(pagedResponse);
        }

        public async Task<Response<RoleResponse>> GetClaimByRole(string id)
        {
            Role role = await _roleManager.Roles
                .FirstAsync(role => role.Id == id);

            if(role is null)
                return ResponseHelper.CreateErrorResponse<RoleResponse>
                       (404, "No role found.");

            var claims = await _roleManager.GetClaimsAsync(role);

            RoleResponse newRole =  new RoleResponse();

            newRole.Claims = claims;
            newRole.Id = id;
            newRole.NormalizedName = role.NormalizedName;
            newRole.Name = role.Name;

            return ResponseHelper.CreateSuccessResponse(newRole);
        }

        public async Task<Response<bool>> AddClaim(IdentityRoleClaim<string> claim)
        {
            Role role = await _roleManager.Roles
                .FirstAsync(role => role.Id == claim.RoleId);

            if (role is null)
                return ResponseHelper.CreateErrorResponse<bool>
                       (404, "No role found.");

            Claim newClaim = new Claim(claim.ClaimType!, claim.ClaimValue!);
            var result = await _roleManager.AddClaimAsync(role, newClaim);
            
            if(!result.Succeeded)
                return ResponseHelper
                        .CreateErrorResponse<bool>
                        (500, "The server cannot process the request for an unknown reason");

            return ResponseHelper.CreateSuccessResponse(true);
        }

    }
}
