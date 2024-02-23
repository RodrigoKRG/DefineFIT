using DefineFIT.Application.Services.Interfaces;
using DefineFIT.Domain.Common.Exceptions;
using DefineFIT.Domain.Common.Handlers;
using DefineFIT.Domain.Entities;
using DefineFIT.Domain.Helpers;
using DefineFIT.Domain.Repositories;
using DefineFIT.Domain.Requests;
using DefineFIT.Domain.Responses;
using DefineFIT.Domain.Validators;
using Microsoft.Extensions.Logging;
using InvalidDataException = DefineFIT.Domain.Common.Exceptions.InvalidDataException;


namespace DefineFIT.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<UserResponse?> CreateAsync(UserCreateRequest request)
        {
            var validator = new UserCreateValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogError($"User validation failed: {validationResult.Errors}");
                throw ExceptionHandler.CreateException<InvalidDataException>(
                    message: validationResult.Errors.First().ToString(),
                    _logger
                 );
            }
            var user = User.Create(request);
            
            var salt = PasswordHashHelper.GenerateSalt();
            var hashPassword = PasswordHashHelper.GenerateHashPassword(request.Password, salt);
            user.SetPassword(hashPassword, salt);

            var result = await _userRepository.AddAsync(user);
            return UserResponse.Build(result);
        }

        public async Task<UserResponse> UpdateAsync(long id, UserUpdateRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                _logger.LogError($"User with id {id} not found.");
                throw ExceptionHandler.CreateException<EntityNotFoundException>(
                    message: "Usuário com id {0} não encontrado.",
                    _logger,
                    parameters: new string[] { id.ToString() });
            }

            user.Update(request);
            var result = await _userRepository.UpdateAsync(user);
            return UserResponse.Build(result);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                _logger.LogError($"User with id {id} not found.");
                throw ExceptionHandler.CreateException<EntityNotFoundException>(
                    message: "Usuário com id {0} não encontrado.",
                    _logger,
                    parameters: new string[] { id.ToString() }
                );
            }

            return await _userRepository.RemoveAsync(user);
        }

        public async Task<List<UserResponse>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(UserResponse.Build).ToList();
        }

        public async Task<UserResponse?> GetByIdAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user is null ? null : UserResponse.Build(user);
        }
    }
}
