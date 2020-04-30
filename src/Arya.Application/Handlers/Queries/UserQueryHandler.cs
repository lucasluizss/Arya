using Arya.Application.Domain.Factories;
using Arya.Application.Domain.Queries.User;
using Arya.Application.ViewModels.User;
using Arya.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tyrion;

namespace Arya.Application.Handlers.Queries
{
    public sealed class UserQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>,
                                           IRequestHandler<GetUserByEmailQuery, UserViewModel>,
                                           IRequestHandler<GetAllUsersQuery, IEnumerable<UserViewModel>>
    {
        private readonly IUserService _userService;

        public UserQueryHandler(IUserService userService) => _userService = userService;

        public async Task<IResult<UserViewModel>> Execute(GetUserByIdQuery request)
        {
            var response = await _userService.Get(request.Id);

            return await Result<UserViewModel>.SuccessAsync(UserFactory.Create(response));
        }

        public async Task<IResult<UserViewModel>> Execute(GetUserByEmailQuery request)
        {
            var response = await _userService.GetByEmail(request.Email);

            return await Result<UserViewModel>.SuccessAsync(UserFactory.Create(response));
        }

        public async Task<IResult<IEnumerable<UserViewModel>>> Execute(GetAllUsersQuery request)
        {
            var response = await _userService.GetAll();

            return await Result<IEnumerable<UserViewModel>>.SuccessAsync(response.Select(user => UserFactory.Create(user)));
        }
    }
}
