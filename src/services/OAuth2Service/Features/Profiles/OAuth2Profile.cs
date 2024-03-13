namespace OAuth2Service.Profiles
{
    public class OAuth2Profile : Profile
    {
        public OAuth2Profile()
        {
            CreateMap<RoleRequest, Role>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Role, RoleResponse>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<RoleRequest>>, PaginationResponse<List<Role>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<Role>>, PaginationResponse<List<RoleResponse>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<PermissionRequest, Permission>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Permission, PermissionResponse>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<PermissionRequest>>, PaginationResponse<List<Permission>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<Permission>>, PaginationResponse<List<PermissionResponse>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UserRequest, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<User, UserResponse>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<UserRequest>>, PaginationResponse<List<User>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<User>>, PaginationResponse<List<UserResponse>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<GroupRequest, Group>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Group, GroupResponse>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<GroupRequest>>, PaginationResponse<List<Group>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<Group>>, PaginationResponse<List<GroupResponse>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AssignmentRequest, Assignment>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Assignment, AssignmentResponse>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<AssignmentRequest>>, PaginationResponse<List<Assignment>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<Assignment>>, PaginationResponse<List<AssignmentResponse>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<RegisterRequest, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
