using BaseSource.Model;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthenticateRepository
    {
        Task<IdentityResult> CreateUserAsync(User user, string password, string role);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<User> ReadUserAsync(string id);
        Task<User> UserExists(string username);
        Task<User?> EmailExists(string email);
        Task<bool> IsPasswordValid(User user, string password);
        Task<IList<string>> GetRolesByUser(User user);
        Task<IdentityResult> CreateUserRoleAsync(User user, string role);
    }

    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticateRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password, string role)
        {
            //Create new user
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return result;

            //And set role
            await CreateUserRoleAsync(user, role);

            return result;
        }

        public async Task<User> ReadUserAsync(string id)
        {
            var user =  await _userManager.FindByIdAsync(id);
            if (user == null)
                return null!;
            return user;
        }

        public async Task<User> UserExists(string username)
        {
            //Find user by username
            var userExists = await _userManager.FindByNameAsync(username);
            
            if (userExists == null)
                return null!;
            return userExists;

        }
        public async Task<User?> EmailExists(string email)
        {
            //Find user by email
            var emailExists = await _userManager.FindByEmailAsync(email);
            if (emailExists == null)
                return null;
            return emailExists;
        }

        public async Task<bool> IsPasswordValid(User user, string password)
        {
            //Return true if password validate ,else false
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetRolesByUser(User user)
        {
            //Return all roles of user have (Customer,Staff,...)
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> CreateUserRoleAsync(User user, string role)
        {
            //All users create default role are customer, if login via google, fb or something else then visitor
            //Only admin role have permission to update role for user
            //Only super admin has permission to update admin
            //Only 1 super admin account
            return await _userManager.AddToRoleAsync(user, role.ToUpper());
        }

    }
}
