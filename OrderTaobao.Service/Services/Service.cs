﻿using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Services
{
    public interface IService<T> where T : BaseEntity
    {
        Task<Response<List<T>>> Get();
        Task<Response<PageResponse<List<T>>>> GetPagedData([FromQuery] PaginationRequest request, string route, bool enable);
        Task<Response<T>> GetById(string id);
        Task<Response<bool>> Add(string user,T request);
        Task<Response<bool>> Update(string id, string user,T request);
        Task<Response<bool>> Enable(string id, string user, bool enable);
        Task<Response<bool>> Erase(string id, string user);
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

        public async Task<Response<bool>> Add(string user,T request)
        {
            await _repository.AddAsync(request, user);

            return ResponseHelper.CreateCreatedResponse(true);
        }

        public async Task<Response<bool>> Update(string id,string user,T request)
        {
            T item = await _repository.ReadByIdAsync(id);

            if (item is null)
                return ResponseHelper.CreateNotFoundResponse<bool>(typeof(T).FullName!);

            item = request;

            await _repository.UpdateAsync(item, user);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<Response<bool>> Enable(string id, string user, bool enable)
        {
            T item = await _repository.ReadByIdAsync(id);

            if (item is null)
                return ResponseHelper.CreateNotFoundResponse<bool>(typeof(T).FullName!);

            item.Enable = enable;

            await _repository.UpdateAsync(item, user);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<Response<bool>> Erase(string id, string user)
        {
            T item = await _repository.ReadByIdAsync(id);

            if (item is null)
                return ResponseHelper.CreateNotFoundResponse<bool>(typeof(T).FullName!);

            await _repository.DeleteAsync(item, user);

            return ResponseHelper.CreateSuccessResponse(true);
        }
    }
}