using Arya.Domain.Entitties;
using System.Threading.Tasks;

namespace Arya.Domain.Interfaces
{
    public interface IUserService : IServiceBase<UserEntity>
    {
        Task<(UserEntity User, string Token)> Authenticate(string email, string password);
        Task<UserEntity> GetByEmail(string email);
    }
}
