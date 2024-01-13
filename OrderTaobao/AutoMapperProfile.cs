using AutoMapper;

namespace BaseSource.BackendAPI
{
    public class AutoMapperProfile<T, TRequest, TResponse> : Profile where T : BaseEntity
    {
        public AutoMapperProfile() 
        {
            CreateMap<CategoryRequest, Category>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, context) => srcMember != null));
            CreateMap<Category, CategoryResponse>();

            CreateMap<UserRequest, User>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, context) => srcMember != null));
            CreateMap<User, UserResponse>();

            CreateMap<PageResponse<List<Category>>, PageResponse<List<CategoryResponse>>>();

            CreateMap<PageResponse<List<T>>, PageResponse<List<TResponse>>>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, context) => srcMember != null));

        }

        public void ConfigureAutoMapper()
        {
            // Other AutoMapper configurations...

            // Set up the mapping for Category to CategoryResponse
            CreateMap<Category, CategoryResponse>();

            // Set up the mapping for PageResponse<List<Category>> to PageResponse<List<CategoryResponse>>
            CreateMap<PageResponse<List<Category>>, PageResponse<List<CategoryResponse>>>();

            // Other AutoMapper configurations...
        }
    }
}
