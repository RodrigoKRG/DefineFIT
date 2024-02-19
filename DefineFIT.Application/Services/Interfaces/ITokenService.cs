using DefineFIT.Domain.Entities;
using DefineFIT.Domain.Requests;

namespace DefineFIT.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(LoginRequest request);
    }
}
