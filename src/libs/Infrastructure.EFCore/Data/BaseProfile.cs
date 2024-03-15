namespace Infrastructure.EFCore
{
    public class BaseProfile<TEntity, TRequest, TResponse> : Profile
    {
        public BaseProfile()
        {
            CreateMap<TRequest, TEntity>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<TEntity, TResponse>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AbstractFile, TEntity>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<TRequest>>, PaginationResponse<List<TEntity>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<TEntity>>, PaginationResponse<List<TResponse>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
