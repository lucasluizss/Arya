using Arya.Domain.Entities;
using Arya.Domain.Interfaces;
using GoldenCompany;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Arya.Service.Services
{
    public sealed class UserService : ServiceBase<UserEntity>, IUserService
    {
        private readonly IConfiguration Configuration;
        private readonly IRepositoryBase<UserEntity> UserRepository;

        public UserService(
            IConfiguration configuration,
            IRepositoryBase<UserEntity> userRepository
        ) : base(userRepository)
        {
            Configuration = configuration;
            UserRepository = userRepository;
        }

        public async Task<(UserEntity User, string Token)> Authenticate(string email, string password)
        {
            var encryptPassword = Cryptography.Encrypt(password);

            var user = await UserRepository.Get(where => where.Email.Address.Equals(email) && where.Password.Equals(encryptPassword));

            if (user == default)
            {
                return default;
            }

            var token = GetSecurityToken(user);

            return (user, token);
        }

        public async Task<UserEntity> GetByEmail(string email) => await UserRepository.Get(predicate => predicate.Email.Address.Equals(email));

        public async Task<bool> UserAlreadyExists(string email) => await UserRepository.Any(predicate => predicate.Email.Address.Equals(email));

        private string GetSecurityToken(UserEntity user)
        {
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("Auth:SecurityKey").Value);
            var expireDays = Convert.ToInt32(Configuration.GetSection("Auth:TokenExpireDays").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(expireDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
