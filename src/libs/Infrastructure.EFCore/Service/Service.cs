namespace Infrastructure.EFCore.Service
{
    public class Service<TEntity, TRequest, TResponse> : IService<TEntity, TRequest, TResponse> where TEntity : Entity where TRequest : EntityRequest
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public Service(IRepository<TEntity> repository, IMapper mapper, IUriService uriService)
        {
            _repository = repository;
            _mapper = mapper;
            _uriService = uriService;
        }
        
        public async Task<Response<PaginationResponse<List<TResponse>>>> FindPageAsync(PaginationRequest request, string route)
        {
            var entities = await _repository.FindPageAsync(request, route, _uriService);

            if (entities is null)
                return ResponseHelper.CreateNotFoundResponse<PaginationResponse<List<TResponse>>>(null);

            PaginationResponse<List<TResponse>> response = _mapper.Map<PaginationResponse<List<TResponse>>>(entities);

            return ResponseHelper.CreateSuccessResponse(response);
        }

        public async Task<Response<List<TResponse>>> FindAllAsync(params string[] properties)
        {
            List<TEntity> records = await _repository.FindAllAsync(properties);

            if (records is null)
                return ResponseHelper.CreateNotFoundResponse<List<TResponse>>(null);

            records = records.Where(record => record.Enable).ToList();

            List<TResponse> response = _mapper.Map<List<TResponse>>(records);

            return ResponseHelper.CreateSuccessResponse(response);
        }

        public async Task<Response<TResponse>> FindByIdAsync(Guid id)
        {
            TEntity record = await _repository.FindByIdAsync(id);

            if (record is null)
                return ResponseHelper.CreateNotFoundResponse<TResponse>(null);

            var response = _mapper.Map<TResponse>(record);

            return ResponseHelper.CreateSuccessResponse(response);
        }

        public async Task<Response<TResponse>> FindOneAsync(Expression<Func<TEntity, bool>>[] conditions)
        {
            TEntity record = await _repository.FindOneAsync(conditions);

            if (record is null)
                return ResponseHelper.CreateNotFoundResponse<TResponse>(null);

            TResponse response = _mapper.Map<TResponse>(record);

            return ResponseHelper.CreateSuccessResponse(response);
        }

        public async Task<Response<TResponse>> AddAsync(TRequest request)
        {
            TEntity entity = _mapper.Map<TEntity>(request);

            TEntity record = await _repository.AddAsync(entity);

            TResponse response = _mapper.Map<TResponse>(record);

            return ResponseHelper.CreateCreatedResponse(response);
        }

        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            TEntity record = await _repository.FindByIdAsync(id);

            if (record is null)
                return ResponseHelper.CreateNotFoundResponse<bool>(null);

            await _repository.DeleteAsync(record);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<Response<TResponse>> EditAsync(Guid id, TRequest request)
        {
            if (id != request.Id)
            {
                return ResponseHelper.CreateNotFoundResponse<TResponse>(null);
            }

            TEntity record = await _repository.FindByIdAsync(id);
            if (record == null)
            {
                return ResponseHelper.CreateNotFoundResponse<TResponse>(null);
            }

            _mapper.Map(request, record);

            TEntity result = await _repository.EditAsync(record);

            TResponse response = _mapper.Map<TResponse>(result);

            return ResponseHelper.CreateSuccessResponse(response);
        }


        public async Task<Response<List<TResponse>>> BulkEditAsync(List<TRequest> requests)
        {
            List<TEntity> entities = _mapper.Map<List<TEntity>>(requests);

            List<TEntity> records = await _repository.BulkEditAsync(entities);

            List<TResponse> response = _mapper.Map<List<TResponse>>(records);

            return ResponseHelper.CreateSuccessResponse(response);
        }

        public async Task<Response<bool>> BulkDeleteAsync(List<TRequest> requests)
        {
            List<TEntity> entities = _mapper.Map<List<TEntity>>(requests);

            await _repository.BulkDeleteAsync(entities);

            return ResponseHelper.CreateSuccessResponse(true);
        }
    }
}
