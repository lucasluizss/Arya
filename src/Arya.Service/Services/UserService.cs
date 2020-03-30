using Arya.Domain.Entitties;
using Arya.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GoldenCompany;

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

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecurityKey"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(Configuration.GetValue<int>("TokenExpireDays")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return (user, token);
        }

        public async Task<UserEntity> GetByEmail(string email) => await UserRepository.Get(where => where.Email.Equals(email));
    }
}
