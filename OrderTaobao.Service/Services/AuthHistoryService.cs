﻿
using BaseSource.Model;
using Microsoft.Extensions.Configuration;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthHistoryService
    {
        Task CreateAuthHistory(User user, string content);
    }
    public class AuthHistoryService : IAuthHistoryService
    {
        IRepository<AuthHistory> _rootRepo;
        IConfiguration _configuration;
        public AuthHistoryService(IRepository<AuthHistory> rootRepo, IConfiguration configuration) 
        {
            _configuration = configuration;
            _rootRepo = rootRepo;
        }
        public async Task CreateAuthHistory(User user,string content)
        {
            AuthHistory history = new AuthHistory();
            history.UserId = user.Id;
            history.Content = content;
            await _rootRepo.Create(history,user.UserName!);
        }
    }
}