using AutoMapper;
using BaseSource.BackendAPI.Services.Helpers;
using BaseSource.Dto;
using BaseSource.Dto.Request;
using BaseSource.Dto.Response;
using BaseSource.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BaseSource.BackendAPI.Services
{
    public interface IService<T,TRequest, TResponse> where T : BaseEntity
    {
        Task<Response<List<T>>> Get();

        Task<Response<PageResponse<List<T>>>> GetPagedData([FromQuery] PaginationRequest request, string route, params Expression<Func<T, object>>[] properties);

        Task<Response<TResponse>> GetById(string id, params string[] properties);

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

        public async Task<Response<PageResponse<List<T>>>> GetPagedData([FromQuery] PaginationRequest request, string route, params Expression<Func<T, object>>[] properties)
        {
            PageResponse<List<T>> records = await _repository.GetPagedDataAsync(request, route, _uriService, properties);

            return ResponseHelper.CreateSuccessResponse(records);
        }

        public async Task<Response<List<T>>> Get()
        {
            var records = await _repository.ReadAllAsync();

            if (records is null)
                return ResponseHelper.CreateNotFoundResponse<List<T>>(typeof(T).Name!);

            records = records.Where(record => record.Enable).ToList();

            return ResponseHelper.CreateSuccessResponse(records);
        }

        public async Task<Response<TResponse>> GetById(string id, params string[] properties)
        {
            T record = await _repository.ReadByIdAsync(id,properties);

            if (record is null)
                return ResponseHelper.CreateNotFoundResponse<TResponse>(typeof(T).Name!);

            var response = _mapper.Map<TResponse>(record);

            return ResponseHelper.CreateSuccessResponse(response);
        }

        public async Task<Response<TResponse>> Add(TRequest request)
        {
            T record = _mapper.Map<T>(request);

            await _repository.AddAsync(record);

            TResponse response = _mapper.Map<TResponse>(record);

            return ResponseHelper.CreateCreatedResponse(response);
        }

        public async Task<Response<TResponse>> Update(string id,TRequest request)
        {
            T record = await _repository.ReadByIdAsync(id);

            if (record is null)
                return ResponseHelper.CreateNotFoundResponse<TResponse>(typeof(T).Name!);

            record = _mapper.Map(request,record);

            await _repository.UpdateAsync(record);

            TResponse response = _mapper.Map<TResponse>(record);

            return ResponseHelper.CreateSuccessResponse(response);
        }

        public async Task<Response<IList<string>>> MultipleUpdate(MultipleRequest request)
        {
            IList<string> response = new List<string>();

            Parallel.ForEach(request.Ids, async id =>
            {
                T record = await _repository.ReadByIdAsync(id);
                if (record is null)
                {
                    response.Add($"{id} : Fail");
                }
                else
                {
                    record.Enable = request.Enable;
                    response.Add($"{id} : Pass");
                    await _repository.DeleteAsync(record);
                }
            });

            return ResponseHelper.CreateSuccessResponse(response);
        }

        public async Task<Response<bool>> Erase(string id)
        {
            T record = await _repository.ReadByIdAsync(id);

            if (record is null)
                return ResponseHelper.CreateNotFoundResponse<bool>(typeof(T).Name!);

            await _repository.EraseAsync(record);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<Response<IList<string>>> MultipleErase(MultipleRequest request)
        {
            IList<string> responseList = new List<string>();

            foreach (string id in request.Ids)
            {
                T record = await _repository.ReadByIdAsync(id);
                if (record is null)
                {
                    responseList.Add($"{id} : Fail");
                }
                else
                {
                    responseList.Add($"{id} : Pass");
                    await _repository.EraseAsync(record);
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
