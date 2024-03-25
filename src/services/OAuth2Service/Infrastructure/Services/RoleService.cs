using Infrastructure.EFCore.Repository;

namespace OAuth2Service.Services
{
    public interface IRoleService : IService<Role,RoleRequest,RoleResponse>
    {
        Task<Response<List<Role>>> FindAllRolesByUserId(Guid userId);
    }

    public class RoleService : Service<Role, RoleRequest, RoleResponse>, IRoleService
    {
        private readonly IRepository<Group> _groupRepository;

        public RoleService(IRepository<Group> groupRepository, IRepository<Role> roleRepository, IMapper mapper, IUriService uriService)
            : base(roleRepository, mapper, uriService)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Response<List<Role>>> FindAllRolesByUserId(Guid userId)
        {
            var groups = await _groupRepository.FindAllByConditionAsync(conditions: new Expression<Func<Group, bool>>[]
                                                                    {
                                                                        g => g.UserId == userId
                                                                    }, properties: "Role");

            if (groups.Count is 0 || groups is null)
                return ResponseHelper.CreateNotFoundResponse<List<Role>>("No roles found for the specified user.");

            var roles = groups.Select(g => g.Role).Distinct().ToList();

            return ResponseHelper.CreateSuccessResponse(roles);
        }
    }
}
