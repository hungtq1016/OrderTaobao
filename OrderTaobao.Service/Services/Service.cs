using BaseSource.BackendAPI.Services.Helpers;
using BaseSource.Dto;
using BaseSource.Dto.Request;
using BaseSource.Dto.Response;
using BaseSource.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Services
{
    public interface IService<T> where T : BaseEntity
    {
        Task<Response<List<T>>> Get();

        Task<Response<PageResponse<List<T>>>> GetPagedData([FromQuery] PaginationRequest request, string route, bool enable);

        Task<Response<T>> GetById(string id);

        Task<Response<bool>> Add(T request);

        Task<Response<bool>> Update(string id, T request);

        Task<Response<bool>> Enable(string id, bool enable);

        Task<Response<IList<string>>> MultipleEnable(MultipleRequest request, bool enable);

        Task<Response<bool>> Erase(string id);

        Task<Response<IList<string>>> MultipleErase(MultipleRequest request);

        Task<ExcelResponse> Export();

        Task<Response<bool>> Import(IFormFile file);
    }

    public class Service<T> : IService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;
        private readonly IUriService _uriService;

        public Service(IRepository<T> repository, IUriService uriService)
        {
            _repository = repository;
            _uriService = uriService;
        }

        public async Task<Response<PageResponse<List<T>>>> GetPagedData([FromQuery] PaginationRequest request, string route, bool enable)
        {
            PageResponse<List<T>> items = await _repository.GetPagedDataAsync(request, route, _uriService, enable);

            return ResponseHelper.CreateSuccessResponse(items);
        }

        public async Task<Response<List<T>>> Get()
        {
            var items = await _repository.ReadAllAsync();

            if (items is null)
                return ResponseHelper.CreateNotFoundResponse<List<T>>(typeof(T).FullName!);

            items = items.Where(item => item.Enable).ToList();

            return ResponseHelper.CreateSuccessResponse(items);
        }

        public async Task<Response<T>> GetById(string id)
        {
            T item = await _repository.ReadByIdAsync(id);

            if (item is null)
                return ResponseHelper.CreateNotFoundResponse<T>(typeof(T).FullName!);

            return ResponseHelper.CreateSuccessResponse(item);
        }

        public async Task<Response<bool>> Add(T request)
        {
            await _repository.AddAsync(request);

            return ResponseHelper.CreateCreatedResponse(true);
        }

        public async Task<Response<bool>> Update(string id,T request)
        {
            T item = await _repository.ReadByIdAsync(id);

            if (item is null)
                return ResponseHelper.CreateNotFoundResponse<bool>(typeof(T).FullName!);

            item = request;

            await _repository.UpdateAsync(item);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<Response<bool>> Enable(string id, bool enable)
        {
            T item = await _repository.ReadByIdAsync(id);

            if (item is null)
                return ResponseHelper.CreateNotFoundResponse<bool>(typeof(T).FullName!);

            item.Enable = enable;

            await _repository.UpdateAsync(item);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<Response<IList<string>>> MultipleEnable(MultipleRequest request, bool enable)
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
                    item.Enable = enable;
                    responseList.Add($"{id} : Pass");
                    await _repository.DeleteAsync(item);
                }
            }

            return ResponseHelper.CreateSuccessResponse(responseList);

        }

        public async Task<Response<bool>> Erase(string id)
        {
            T item = await _repository.ReadByIdAsync(id);

            if (item is null)
                return ResponseHelper.CreateNotFoundResponse<bool>(typeof(T).FullName!);

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
            string reportname = typeof(T).FullName!;

            var list = await Get();

            if (list.Data.Count > 0)
            {
                var exportbytes = FileHelper.ExportToExcel(list.Data, reportname);

                return new ExcelResponse
                {
                    File = exportbytes,
                    Extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    Name = reportname
                };
            }

            return null;
        }

        public async Task<Response<bool>> Import(IFormFile file)
        {
            string folder = $"Excel\\{typeof(T).FullName!}";
            List<IFormFile> files = new List<IFormFile>();

            files.Add(file);

            var result = await FileHelper.WriteFile(files, folder, "xlsx");

            if (result is null)
            {
                return ResponseHelper.CreateNotFoundResponse<bool>(typeof(T).FullName!);
            }
            return ResponseHelper.CreateSuccessResponse(true);
        }
    }
}
