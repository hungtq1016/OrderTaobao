

using BaseScource.Data;
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.BackendAPI.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Customers.AnyAsync(c => c.UserName == username))
                return true;
            else
                return false;
        }

        public async Task<Customer> Login(LoginDto request)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserName == request.UserName);

            if (customer == null)
                return null!;
            return customer;
        }

        public async Task<Customer> Register(RegisterDto request, string hashPassword)
        {
            Customer newCustomer = new Customer();

            newCustomer.Id = Guid.NewGuid();
            newCustomer.FirstName = request.FirstName;
            newCustomer.LastName = request.LastName;
            newCustomer.UserName = request.UserName.ToLower();
            newCustomer.Phone = request.Phone;
            newCustomer.Email = request.Email;
            newCustomer.RememberToken = Guid.NewGuid();
            newCustomer.Enable = true;
            newCustomer.Password = hashPassword;

            await _context.Customers.AddAsync(newCustomer);
          
            Save();

            return newCustomer;
        }

        public async Task<string> CreateToken(Guid customerId,string token)
        {
            AccessToken newToken = new AccessToken();

            newToken.Id = Guid.NewGuid();
            newToken.Token = token;
            newToken.LastUsedAt = DateTime.Now;
            newToken.CreatedAt = DateTime.Now;
            newToken.ExpireAt = DateTime.Now.AddDays(7);
            newToken.CustomerId = customerId;
           
            await _context.AccessToken.AddAsync(newToken);
            Save();
            
            return newToken.Token;
           
        }

        public async Task<string> GenerateToken(Guid customerId, string t)
        {
            var token = await _context.AccessToken.OrderByDescending(ac => ac.CreatedAt).FirstOrDefaultAsync(ac => ac.CustomerId == customerId);
            if (token == null)
            {
                return await CreateToken(customerId,t);
            }
            if (DateTime.Compare(DateTime.Now,token.ExpireAt)>0)
            {
               return await CreateToken(customerId,t);
            }
            else
            {
                token.LastUsedAt = DateTime.Now;
                Save();
                return token.Token;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
