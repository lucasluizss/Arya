using Arya.Domain.Entities;
using Arya.Domain.Interfaces;
using Arya.Infrastructure.Core.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Arya.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IUserService _userService;

        public UnitTest1()
        {
            var services = new ServiceCollection();
            _userService = services.BuildServiceProvider().GetRequiredService<IUserService>();
        }

        [TestMethod]
        public void Save_User()
        {
            var user = new UserEntity("User", new Email("user@tests.com"), "123M@456");

            var response = _userService.Add(user).Result;

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void Get_User_By_Id()
        {
            var user = new UserEntity("User", new Email("user@tests.com"), "123M@456");

            var savedUser = _userService.Add(user).Result;

            var returnedUser = _userService.Get(savedUser.Id).Result;

            Assert.IsNotNull(returnedUser);
        }

        [TestMethod]
        public void Get_Users()
        {
            var returnedUsers = _userService.GetAll().Result;

            Assert.IsNotNull(returnedUsers);
        }

        [TestMethod]
        public void Update_User()
        {
            var user = _userService.Add(new UserEntity("User", new Email("user@tests.com"), "123M@456")).Result;

            var updatedUser = new UserEntity("User", new Email("user@tests.com"), "123M@456");

            var response = _userService.Update(updatedUser).Result;

            Assert.AreEqual(user.Email, response.Email);
        }

        [TestMethod]
        public void Delete_User()
        {
            var new_user = new UserEntity("User", new Email("user@tests.com"), "123M@456");

            var savedUser = _userService.Add(new_user).Result;

            _userService.Remove(savedUser.Id);

            Assert.IsNull(_userService.Get(savedUser.Id).Result);
        }
    }
}
