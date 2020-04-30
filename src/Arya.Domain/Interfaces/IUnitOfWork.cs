using System.Threading.Tasks;

namespace Arya.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
