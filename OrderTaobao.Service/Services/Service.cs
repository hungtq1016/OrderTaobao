using AutoMapper;
using BaseSource.BackendAPI.Services.Helpers;
using BaseSource.Dto;
using BaseSource.Dto.Request;
using BaseSource.Dto.Response;
using BaseSource.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Services
{
    public interface IService<T,TRequest, TResponse> where T : BaseEntity
    {
        Task<Response<List<T>>> Get();

        Task<Response<PageResponse<List<T>>>> GetPagedData([FromQuery] PaginationRequest request, string route);

        Task<Response<T>> GetById(string id);

        Task<Response<TResponse>> Add(TRequest request);

        Task<Response<TResponse>> Update(string id, TRequest request);

        Task<Response<IList<string>>> MultipleUpdate(MultipleRequest request);

        Task<Response<bool>> Erase(string id);

        Task<Response<IList<string>>> MultipleErase(MultipleRequest request);

        Task<ExcelResponse> Export();

        Task<Response<bool>> Import(IFormFile file);
    }

    public class Service<T, TRequest, TResponse> : IService<T, TRequest, TResponse> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public Service(IRepository<T> repository, IUriService uriService, IMapper mapper)
        {
            _repository = repository;
            _uriService = uriService;
            _mapper = mapper;
        }

        public async Task<Response<PageResponse<List<T>>>> GetPagedData([FromQuery] PaginationRequest request, string route)
        {
            PageResponse<List<T>> items = await _repository.GetPagedDataAsync(request, route, _uriService);

            return ResponseHelper.CreateSuccessResponse(items);
        }

        public async Task<Response<List<T>>> Get()
        {
            var items = await _repository.ReadAllAsync();

            if (items is null)
                return ResponseHelper.CreateNotFoundResponse<List<T>>(typeof(T).Name!);

            items = items.Where(item => item.Enable).ToList();

            return ResponseHelper.CreateSuccessResponse(items);
        }

        public async Task<Response<T>> GetById(string id)
        {
            T item = await _repository.ReadByIdAsync(id);

            if (item is null)
                return ResponseHelper.CreateNotFoundResponse<T>(typeof(T).Name!);

            return ResponseHelper.CreateSuccessResponse(item);
        }

        public async Task<Response<TResponse>> Add(TRequest request)
        {
            T item = _mapper.Map<T>(request);

            await _repository.AddAsync(item);

            TResponse response = _mapper.Map<TResponse>(item);

            return ResponseHelper.CreateCreatedResponse(response);
        }

        public async Task<Response<TResponse>> Update(string id,TRequest request)
        {
            T item = await _repository.ReadByIdAsync(id);

            if (item is null)
                return ResponseHelper.CreateNotFoundResponse<TResponse>(typeof(T).Name!);

            item = _mapper.Map(request,item);

            await _repository.UpdateAsync(item);

            TResponse response = _mapper.Map<TResponse>(item);

            return ResponseHelper.CreateSuccessResponse(response);
        }

        public async Task<Response<IList<string>>> MultipleUpdate(MultipleRequest request)
        {
            IList<string> response = new List<string>();

            Parallel.ForEach(request.Ids, async id =>
            {
                T item = await _repository.ReadByIdAsync(id);
                if (item is null)
                {
                    response.Add($"{id} : Fail");
                }
                else
                {
                    item.Enable = request.Enable;
                    response.Add($"{id} : Pass");
                    await _repository.DeleteAsync(item);
                }
            });

            return ResponseHelper.CreateSuccessResponse(response);
        }

        public async Task<Response<bool>> Erase(string id)
        {
            T item = await _repository.ReadByIdAsync(id);

            if (item is null)
                return ResponseHelper.CreateNotFoundResponse<bool>(typeof(T).Name!);

            await _repository.EraseAsync(item);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<Response<IList<string>>> MultipleErase(MultipleRequest request)
        {
            IList<string> responseList = new List<string>();

            foreach (string id in request.Ids)
            {
                T item = await _repository.ReadByIdAsync(id);
                if (item is null)
                {
                    responseList.Add($"{id} : Fail");
                }
                else
                {
                    responseList.Add($"{id} : Pass");
                    await _repository.EraseAsync(item);
                }
            }

            return ResponseHelper.CreateSuccessResponse(responseList);

        }

        public async Task<ExcelResponse> Export()
        {
            string reportname = typeof(T).Name!;

            var list = await Get();

            if (list.Data?.Count > 0)
            {
                var exportbytes = FileHelper.ExportToExcel(list.Data, reportname);

                return new ExcelResponse
                {
                    File = exportbytes,
                    Extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    Name = reportname
                };
            }

            return null!;
        }

        public async Task<Response<bool>> Import(IFormFile file)
        {
            string folder = $"Excel\\{typeof(T).Name!}";
            List<IFormFile> files = new List<IFormFile>();

            files.Add(file);

            var result = await FileHelper.WriteFile(files, folder, "xlsx");

            if (result is null)
            {
                return ResponseHelper.CreateNotFoundResponse<bool>(typeof(T).Name!);
            }
            return ResponseHelper.CreateSuccessResponse(true);
        }
    }
}
