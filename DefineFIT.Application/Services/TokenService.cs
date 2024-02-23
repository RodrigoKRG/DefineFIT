using DefineFIT.Application.Services.Interfaces;
using DefineFIT.Domain.Common.Exceptions;
using DefineFIT.Domain.Common.Handlers;
using DefineFIT.Domain.Entities;
using DefineFIT.Domain.Helpers;
using DefineFIT.Domain.Repositories;
using DefineFIT.Domain.Requests;
using DefineFIT.Domain.Responses;
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

        public async Task<LoginResponse> GenerateToken(LoginRequest request, bool isRefreshToken = false)
        {
            var user = await GetUserByEmail(request.Email);
            var expirationToken = DateTime.Now.AddMinutes(30);
            var expirationRefreshToken = DateTime.Now.AddDays(1);

            if (!isRefreshToken)
                ValidateLogin(request, user);

            if (!user.Active)
            {
                _logger.LogError($"User with email {request.Email} is not active.");
                throw ExceptionHandler.CreateException<InvalidDataException>(message: "Usuário inativo.", _logger);
            }

            var token = GenerateJwtToken(user, expirationToken, true);
            var refreshToken = GenerateJwtToken(user, expirationRefreshToken, false);

            return new LoginResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                TokenExpiration = expirationToken,
                RefreshTokenExpiration = expirationRefreshToken
            };
        }

        private void ValidateLogin(LoginRequest request, User user)
        {
            var hashPassword = PasswordHashHelper.GenerateHashPassword(request.Password, user.Salt);
            if (!hashPassword.Equals(user.Password))
            {
                _logger.LogError($"Password Invalid");
                throw ExceptionHandler.CreateException<InvalidDataException>(
                    message: "Email ou senha invalidos.",
                    _logger
                );
            }
        }

        private async Task<User> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user is null)
            {
                _logger.LogError($"User with email {email} not found.");
                throw ExceptionHandler.CreateException<EntityNotFoundException>(
                    message: "Email não cadastrado.",
                    _logger
                );
            }

            return user;
        }

        private string GenerateJwtToken(User user, DateTime expiration, bool addUserClaims)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: GenerateClaims(user, addUserClaims),
                    expires: expiration,
                    signingCredentials: credentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }

        private static List<Claim> GenerateClaims(User user, bool addUserClaims)
        {
            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Email, value: user.Email),
            };

            if (addUserClaims)
            {
                claims.AddRange(
                [
                    new Claim(type: ClaimTypes.Name, value: user.Name),
                    new Claim(type: ClaimTypes.Email, value: user.Email),
                    new Claim(type: ClaimTypes.Role, value: user.Role.ToString())
                ]);
            }

            return claims;
        }

        public string GetEmail(ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.Email)?.Value!;
    }
}
