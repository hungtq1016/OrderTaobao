﻿using AutoMapper;
using Core;
using Infrastructure.EFCore.DTOs;
using Infrastructure.EFCore.Helpers;
using Infrastructure.EFCore.Repository;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EFCore.Service
{
    public interface IFileService<TEntity, TRequest, TResponse, TExtensionEnum>
        : IService<TEntity, TRequest, TResponse>
        where TEntity : AbstractFile
        where TExtensionEnum : Enum
        where TRequest : EntityRequest
    {
        Task<Response<List<TResponse>>> AddAsync(List<IFormFile> files);
    }
    public class FileService<TEntity, TRequest, TResponse, TExtensionEnum>
        : Service<TEntity, TRequest, TResponse>, IFileService<TEntity, TRequest, TResponse, TExtensionEnum>
        where TEntity : AbstractFile
        where TExtensionEnum : Enum
        where TRequest : EntityRequest
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public FileService(IRepository<TEntity> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<TResponse>>> AddAsync(List<IFormFile> files)
        {
            var records = await FileHelper.WriteFile<TExtensionEnum>(files, typeof(TEntity).Name);

            List<TEntity> entities = new List<TEntity>();

            foreach (var record in records)
            {
                if (record is not null)
                {
                    var entity = _mapper.Map<TEntity>(record);
                    var result = await _repository.AddAsync(entity);
                    entities.Add(result);
                }
            }

            var response =  _mapper.Map<List<TResponse>>(entities);

            return ResponseHelper.CreateCreatedResponse(response);
        }

        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            var file = await _repository.FindByIdAsync(id);

            if (file is null)
                return ResponseHelper.CreateNotFoundResponse<bool>
                       ($"No {typeof(TEntity).Name} Found!");

            await _repository.DeleteAsync(file);

            string filepath = Path.Combine(Directory.GetCurrentDirectory(), $"Upload/{typeof(TEntity).Name}/{file.Path}");

            if (File.Exists(filepath))
                File.Delete(filepath);

            return ResponseHelper.CreateSuccessResponse(true);
        }
    }
}
