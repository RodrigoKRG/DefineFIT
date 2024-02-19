using DefineFIT.Application.Services.Interfaces;
using DefineFIT.Domain.Common.Exceptions;
using DefineFIT.Domain.Common.Handlers;
using DefineFIT.Domain.Entities;
using DefineFIT.Domain.Repositories;
using DefineFIT.Domain.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InvalidDataException = DefineFIT.Domain.Common.Exceptions.InvalidDataException;


namespace DefineFIT.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<TokenService> _logger;



        public TokenService(IConfiguration configuration, IUserRepository userRepository, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<string> GenerateToken(LoginRequest request)
        {
            var user = await GetUserByEmail(request);
            ValidateLogin(request, user);
            return GenerateJwtToken(user);
        }
        private void ValidateLogin(LoginRequest request, User user)
        {
            if (!user.Password.Equals(request.Password))
            {
                _logger.LogError($"Password Invalid");
                throw ExceptionHandler.CreateException<InvalidDataException>(
                    message: "Email ou senha invalidos.",
                    _logger
                );
            }
        }

        private async Task<User> GetUserByEmail(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is null)
            {
                _logger.LogError($"User with email {request.Email} not found.");
                throw ExceptionHandler.CreateException<EntityNotFoundException>(
                    message: "Email não cadastrado.",
                    _logger
                );
            }

            return user;
        }

        private string GenerateJwtToken(User user)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: new[]
                    {
                        new Claim(type: ClaimTypes.Name, value: user.Name),
                        new Claim(type: ClaimTypes.Email, value: user.Email),
                        new Claim(type: ClaimTypes.Role, value: user.Role.ToString())
                    },
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
    }
}
