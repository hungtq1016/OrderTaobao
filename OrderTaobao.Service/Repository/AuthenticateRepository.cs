
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthenticateRepository
    {
        Task<IdentityResult> Register(IdentityUser user, string password, string role);
        Task<IdentityResult> Login(IdentityUser user, UserLoginInfo info);
        Task<IdentityUser?> UserExists(string username);
        Task<IdentityUser?> EmailExists(string email);
        Task<bool> IsPasswordValid(IdentityUser user, string password);
        Task<IList<string>> GetRolesByUser(IdentityUser user);
        Task<IdentityResult> AddRoleForUser(IdentityUser user,string role);
    }

    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthenticateRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(IdentityUser user, string password,string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return result;
            await AddRoleForUser(user, role);
            
            return result;
        }

        public async Task<IdentityResult> Login(IdentityUser user, UserLoginInfo info)
        {
            var result = await _userManager.AddLoginAsync(user, info);
            return result;
        }

        public async Task<IdentityUser?> UserExists(string username)
        {
            var userExists = await _userManager.FindByNameAsync(username);
            if (userExists == null)
                return null;
            return userExists;

        }
        public async Task<IdentityUser?> EmailExists(string email)
        {
            var emailExists = await _userManager.FindByEmailAsync(email);
            if (emailExists == null)
                return null;
            return emailExists;
        }

        public async Task<bool> IsPasswordValid(IdentityUser user ,string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetRolesByUser(IdentityUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> AddRoleForUser(IdentityUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role.ToUpper());
        }

    }
}
