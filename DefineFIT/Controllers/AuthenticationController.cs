using DefineFIT.Application.Services.Interfaces;
using DefineFIT.Domain.Requests;
using DefineFIT.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Retornando um token de autorização
        /// </summary>
        /// <response code="200">Retorna token de autorização</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="401">Erro de autorização</response>
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var response = await _tokenService.GenerateToken(request);
            return Ok(new { response });
        }

        /// <summary>
        /// Retornando um novo token de autorização
        /// </summary>
        /// <response code="200">Retorna token de autorização</response>
        /// <response code="400">Erro de validação</response>
        /// <response code="401">Erro de autorização</response>
        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            var email = _tokenService.GetEmail(User);
            var request = new LoginRequest { Email = email };
            var response = await _tokenService.GenerateToken(request, true);
            return Ok(response);
        }
    }
}
