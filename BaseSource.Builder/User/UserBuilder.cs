
using BaseSource.Model;
using Microsoft.Extensions.Configuration;

namespace BaseSource.Builder
{
    public class UserBuilder
    {
        private User user = new User();
        private readonly IConfiguration _configuration;

        public UserBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public UserBuilder()
        {
            
        }

        public UserBuilder WithId() 
        {
            user.Id = Guid.NewGuid().ToString();
            return this;
        }

        public UserBuilder WithUserName(string userName)
        {
            user.UserName = userName;
            return this;
        }

        public UserBuilder WithFirstName(string firstName)
        {
            user.FirstName = firstName;
            return this;
        }

        public UserBuilder WithLastName(string lastName)
        {
            user.LastName = lastName;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            user.Email = email;
            return this;
        }

        public UserBuilder WithPhone(string phone)
        {
            user.PhoneNumber = phone;
            return this;
        }

        public UserBuilder WithRefreshToken(string refreshToken)
        {
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
            return this;
        }

        public UserBuilder WithEnable()
        {
            user.Enable = true;
            return this;
        }

        public UserBuilder WithDisable()
        {
            user.Enable = false;
            return this;
        }

        public UserBuilder WithSecurityStamp()
        {
            user.SecurityStamp = Guid.NewGuid().ToString();
            return this;
        }

        public User Build() {
            return user;
        }
    }
}
