using Arya.Application.Domain.Commands.User;
using Arya.Application.Domain.Events;
using Arya.Application.Domain.Factories;
using Arya.Application.ViewModels.User;
using Arya.Domain.Interfaces;
using System.Threading.Tasks;
using Tyrion;

namespace Arya.Application.Handlers.Commands
{
    public sealed class UserCommandHandler : IRequestHandler<AddUserCommand, UserViewModel>,
                                             IRequestHandler<UpdateUserCommand, UserViewModel>,
                                             IRequestHandler<RemoveUserCommand>,
                                             IRequestHandler<ActivateUserCommand>,
                                             IRequestHandler<AuthenticateUserCommand, UserViewModel>
    {
        private readonly ITyrion _tyrion;
        private readonly IUserService _userService;

        public UserCommandHandler(ITyrion tyrion, IUserService userService)
        {
            _tyrion = tyrion;
            _userService = userService;
        }

        public async Task<IResult<UserViewModel>> Execute(AddUserCommand request)
        {
            var user = UserFactory.Create(request);

            user.Inactivate();

            var response = await _userService.Add(user);

            await _tyrion.Publish(new SendEmailNewUserEvent(response.Id, response.Name, response.Email.Address)).ConfigureAwait(false);

            return await Result<UserViewModel>.SuccessAsync(UserFactory.Create(response));
        }

        public async Task<IResult<UserViewModel>> Execute(UpdateUserCommand request)
        {
            var response = await _userService.Update(UserFactory.Create(request));

            return await Result<UserViewModel>.SuccessAsync(UserFactory.Create(response));
        }

        public async Task<IResult> Execute(RemoveUserCommand request)
        {
            var response = await _userService.Remove(request.Id);

            return response ? await Result.SuccessAsync() : await Result.FailAsync("Internal process failed, please try again.");
        }

        public async Task<IResult<UserViewModel>> Execute(AuthenticateUserCommand request)
        {
            var response = await _userService.Authenticate(request.Email, request.Password);

            if (response == default)
            {
                return await Result<UserViewModel>.FailAsync("Email or password incorrect.");
            }

            return await Result<UserViewModel>.SuccessAsync(UserFactory.Create(response.User, response.Token));
        }

        public async Task<IResult> Execute(ActivateUserCommand request)
        {
            var user = await _userService.Get(request.Id);

            if (user.IsActive())
            {
                return await Result.FailAsync("User already actived!");
            }

            user.Activate();

            await _userService.Update(user);

            return await Result.SuccessAsync();
        }
    }
}
