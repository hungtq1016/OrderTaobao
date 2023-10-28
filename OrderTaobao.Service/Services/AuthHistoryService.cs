
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
        public AuthHistoryService(IRepository<AuthHistory> rootRepo) 
        {
            _rootRepo = rootRepo;
        }
        public async Task CreateAuthHistory(User user,string content)
        {
            AuthHistory history = new AuthHistory();
            history.UserId = user.Id;
            history.Content = content;
            await _rootRepo.AddAsync(history,user.UserName!);
        }
    }
}
