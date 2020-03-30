using Arya.Application.Domain.Commands.User;
using Arya.Application.ViewModels.User;
using Arya.Domain.Entitties;
using Arya.Infrastructure.Core.Domain.ValueObjects;
using GoldenCompany;

namespace Arya.Application.Domain.Factories
{
    public static class UserFactory
    {
        public static UserEntity Create(AddUserCommand user) => new UserEntity(user.Name, new Email(user.Email), Cryptography.Encrypt(user.Password));

        public static UserEntity Create(UpdateUserCommand user) => new UserEntity(user.Name, new Email(user.Email), Cryptography.Encrypt(user.Password))
        {
            Id = user.Id
        };

        public static UserViewModel Create(UserEntity user) => new UserViewModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email.Address
        };

        public static UserViewModel Create(UserEntity user, string token) => new UserViewModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email.Address,
            Token = token
        };
    }
}
