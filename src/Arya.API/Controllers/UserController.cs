using Arya.Application.Domain.Commands.User;
using Arya.Application.Domain.Queries.User;
using Arya.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tyrion;

namespace Arya.API.Controllers
{
    /// <summary>
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ITyrion _tyrion;

        /// <summary>
        /// </summary>
        public UserController(ITyrion tyrion) => _tyrion = tyrion;

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateUserCommand command)
        {
            try
            {
                return Ok(await _tyrion.Execute<AuthenticateUserCommand, UserViewModel>(command));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="command"></param>
        /// <returns>User object.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromBody]AddUserCommand command)
        {
            try
            {
                return Ok(await _tyrion.Execute<AddUserCommand, UserViewModel>(command));
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User object.</returns>
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            try
            {
                var query = new GetUserByIdQuery(userId);

                return Ok(await _tyrion.Execute<GetUserByIdQuery, UserViewModel>(query));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get user by user email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User object.</returns>
        [Authorize]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var query = new GetUserByEmailQuery(email);

                return Ok(await _tyrion.Execute<GetUserByEmailQuery, UserViewModel>(query));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>List of user's.</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetAllUsersQuery();

                return Ok(await _tyrion.Execute<GetAllUsersQuery, IEnumerable<UserViewModel>>(query));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="command"></param>
        /// <returns>User object.</returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            try
            {
                return Ok(await _tyrion.Execute<UpdateUserCommand, UserViewModel>(command));
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Status code 200.</returns>
        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Remove(Guid userId)
        {
            try
            {
                return Ok(await _tyrion.Execute(new RemoveUserCommand(userId)));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Confirm user email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Confirm")]
        public async Task<IActionResult> Confirm([FromQuery] Guid id)
        {
            try
            {
                return Ok(await _tyrion.Execute(new ActivateUserCommand(id)));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
