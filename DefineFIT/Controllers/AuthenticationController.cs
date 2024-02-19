using DefineFIT.Application.Services.Interfaces;
using DefineFIT.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DefineFIT.Api.Controllers
{
    [Route("api/authentications")]
    public class AuthenticationController : BaseController
    {
        private readonly ITokenService _tokenService;

        public AuthenticationController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var token = await _tokenService.GenerateToken(request);
            return Ok(new { token });
        }
    }
}
