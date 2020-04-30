using Tyrion;

namespace Arya.Application.Domain.Queries.User
{
    public class UserBaseQuery : IRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
