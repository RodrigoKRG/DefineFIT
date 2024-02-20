using DefineFIT.Domain.Requests;
using DefineFIT.Domain.Responses;
using System.Security.Claims;

namespace DefineFIT.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<LoginResponse> GenerateToken(LoginRequest request, bool isRefreshToken = false);
        string GetEmail(ClaimsPrincipal user);
    }
}
