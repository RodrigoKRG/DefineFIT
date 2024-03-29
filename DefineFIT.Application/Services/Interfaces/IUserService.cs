﻿using DefineFIT.Domain.Requests;
using DefineFIT.Domain.Responses;

namespace DefineFIT.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse?> CreateAsync(UserCreateRequest request);
        Task<List<UserResponse>> GetAllAsync();
        Task<UserResponse?> GetByIdAsync(long id);
        Task<UserResponse> UpdateAsync(long id, UserUpdateRequest request);
        Task<bool> DeleteAsync(long id);
    }
}
