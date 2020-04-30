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
                                             IRequestHandler<AuthenticateUserCommand, UserViewModel>,
                                             IRequestHandler<RemoveUserCommand>,
                                             IRequestHandler<ActivateUserCommand>,
                                             IRequestHandler<InactivateUserCommand>
    {
        private readonly ITyrion _tyrion;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public UserCommandHandler(ITyrion tyrion, IUserService userService, IUnitOfWork unitOfWork)
        {
            _tyrion = tyrion;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult<UserViewModel>> Execute(AddUserCommand request)
        {
            if (await _userService.UserAlreadyExists(request.Email))
            {
                return await Result<UserViewModel>.FailAsync("User already exists!");
            }

            var user = UserFactory.Create(request);

            user.Inactivate();

            var userEntity = await _userService.Add(user);

            var success = await _unitOfWork.Commit();

            if (success)
            {
                await _tyrion.Publish(new SendEmailNewUserEvent(userEntity.Id, userEntity.Name, userEntity.Email.Address)).ConfigureAwait(false);

                return await Result<UserViewModel>.SuccessAsync(UserFactory.Create(userEntity));
            }

            return await Result<UserViewModel>.FailAsync("Internal error, please try again.");
        }

        public async Task<IResult<UserViewModel>> Execute(UpdateUserCommand request)
        {
            var userEntity = await _userService.Update(UserFactory.Create(request));

            var success = await _unitOfWork.Commit();

            if (success)
            {
                return await Result<UserViewModel>.SuccessAsync(UserFactory.Create(userEntity));
            }

            return await Result<UserViewModel>.FailAsync("Internal error, please try again.");
        }

        public async Task<IResult> Execute(RemoveUserCommand request)
        {
            await _userService.Remove(request.Id);

            var success = await _unitOfWork.Commit();

            if (success)
            {
                return await Result.SuccessAsync();
            }

            return await Result.FailAsync("Internal error, please try again.");
        }

        public async Task<IResult<UserViewModel>> Execute(AuthenticateUserCommand request)
        {
            var response = await _userService.Authenticate(request.Email, request.Password);

            if (response == default)
            {
                return await Result<UserViewModel>.FailAsync("Email or password incorrect.");
            }
            else if (response.User.IsInactive())
            {
                return await Result<UserViewModel>.FailAsync("User not actived, please check your email.");
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

            return await _unitOfWork.Commit() ? Result.Success() : Result.Fail("Internal error, please try again.");
        }

        public async Task<IResult> Execute(InactivateUserCommand request)
        {
            var user = await _userService.Get(request.Id);

            if (user.IsInactive())
            {
                return await Result.FailAsync("User already inactive!");
            }

            user.Inactivate();

            await _userService.Update(user);

            return await _unitOfWork.Commit() ? Result.Success() : Result.Fail("Internal error, please try again.");
        }
    }
}
