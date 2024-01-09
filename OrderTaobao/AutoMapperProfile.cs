using AutoMapper;

namespace BaseSource.BackendAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<CategoryRequest, Category>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, context) => srcMember != null));
            CreateMap<Category, CategoryResponse>();

            CreateMap<UserRequest, User>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, context) => srcMember != null));
            CreateMap<User, UserResponse>();

        }
    }
}
