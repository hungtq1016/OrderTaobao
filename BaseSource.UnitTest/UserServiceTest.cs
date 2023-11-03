using BaseScource.Data;
using BaseSource.BackendAPI.Services;
using BaseSource.Model;
using Bogus;
using Moq;

namespace BaseSource.UnitTest
{
    [TestClass]
    public class UserServiceTest 
    {
        [TestMethod]
        public void AddNewUser()
        {
            var data = GenerateData(1);
            var mockContext = new Mock<DataContext>();
         /*   mockContext.Setup(context => context.Users).Returns(data.FirstOrDefault());*/
        }

        private List<User> GenerateData(int count)
        {
            var data = new Faker<User>()
                .RuleFor(user => user.Id, Guid.NewGuid().ToString())
                .RuleFor(user => user.FirstName, faker => faker.Person.FirstName)
                .RuleFor(user => user.LastName, faker => faker.Person.LastName)
                .RuleFor(user => user.UserName, faker => faker.Person.UserName)
                .RuleFor(user => user.RefreshToken, TokenHelper.GenerateRefreshToken())
                .RuleFor(user => user.RefreshTokenExpiryTime, DateTime.Now.AddDays(7))
                .RuleFor(user => user.Enable, true)
                .RuleFor(user => user.Email, faker => faker.Person.Email)
                .RuleFor(user => user.PhoneNumber, faker => faker.Person.Phone)
                ;

            return data.Generate(count);
        }
    }
}