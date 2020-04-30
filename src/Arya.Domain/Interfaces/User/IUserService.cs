using Arya.Domain.Entities;
using System.Threading.Tasks;

namespace Arya.Domain.Interfaces
{
    public interface IUserService : IServiceBase<UserEntity>
    {
        Task<(UserEntity User, string Token)> Authenticate(string email, string password);

        Task<UserEntity> GetByEmail(string email);

        Task<bool> UserAlreadyExists(string email);
    }
}
