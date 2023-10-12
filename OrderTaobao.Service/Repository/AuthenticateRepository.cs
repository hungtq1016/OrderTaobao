
using System.Security.Claims;
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthenticateRepository
    {
        Task<IdentityResult> Register(IdentityUser user, string password, string role);
        Task<IdentityUser?> UserExists(string username);
        Task<IdentityUser?> EmailExists(string email);
        Task<bool> IsPasswordValid(IdentityUser user, string password);
        Task<IList<string>> GetRolesByUser(IdentityUser user);
        Task<IdentityResult> CreateToken(IdentityUser user, string LoginProvider, TokenResponse token);
        Task<IdentityResult> AddRoleForUser(IdentityUser user,string role);
        Task<IdentityResult> AddToHistoryLogin(IdentityUser user, UserLoginInfo info);
        Task<IdentityResult> RemoveFromHistoryLogin(IdentityUser user, UserLoginInfo info);
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
            //Create new user
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return result;

            //And set role
            await AddRoleForUser(user, role);
            
            return result;
        }

        public async Task<IdentityResult> CreateToken(IdentityUser user, string LoginProvider, TokenResponse token)
        {
            //Save token of user like refesh_token, bearer
            return await _userManager.SetAuthenticationTokenAsync(user,LoginProvider,token.Type,token.Value);
        }

        public async Task<IdentityUser?> UserExists(string username)
        {
            //Find user by username
            var userExists = await _userManager.FindByNameAsync(username);
            if (userExists == null)
                return null;
            return userExists;

        }
        public async Task<IdentityUser?> EmailExists(string email)
        {
            //Find user by email
            var emailExists = await _userManager.FindByEmailAsync(email);
            if (emailExists == null)
                return null;
            return emailExists;
        }

        public async Task<bool> IsPasswordValid(IdentityUser user ,string password)
        {
            //Return true if password validate ,else false
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetRolesByUser(IdentityUser user)
        {
            //Return all roles of user have (Customer,Staff,...)
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> AddRoleForUser(IdentityUser user, string role)
        {
            //All users create default role are customer, if login via google, fb or something else then visitor
            //Only admin role have permission to update role for user
            //Only super admin has permission to update admin
            //Only 1 super admin account
            return await _userManager.AddToRoleAsync(user, role.ToUpper());
        }

        public async Task<IdentityResult> AddToHistoryLogin(IdentityUser user, UserLoginInfo info)
        {
            //Save history login
            //New feature: Auto remove if expired
            return await _userManager.AddLoginAsync(user,info);
        }

        public async Task<IdentityResult> RemoveFromHistoryLogin(IdentityUser user, UserLoginInfo info)
        {
            //Remove if have callback
            return await _userManager.RemoveLoginAsync(user, info.LoginProvider, info.ProviderKey);
        }
    }
}
