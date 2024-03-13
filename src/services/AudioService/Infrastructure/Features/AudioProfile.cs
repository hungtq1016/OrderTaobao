namespace AudioService.Features
{
    public class AudioProfile : Profile
    {
        public AudioProfile()
        {
            CreateMap<AudioRequest, Audio>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Audio, AudioResponse>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AbstractFile, Audio>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<AudioRequest>>, PaginationResponse<List<Audio>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<Audio>>, PaginationResponse<List<AudioResponse>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AlbumRequest, Album>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Album, AlbumResponse>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<AlbumRequest>>, PaginationResponse<List<Album>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PaginationResponse<List<Album>>, PaginationResponse<List<AlbumResponse>>>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
