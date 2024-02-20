using DefineFIT.Application.Services.Interfaces;
using DefineFIT.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DefineFIT.Api.Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize(Roles = "Master, Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Master, Admin")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreateRequest request)
        {
            if (!request.IsValid())
                return HandleValidationErrors(request.ValidationResult);

            request.SetUser(request.Email);

            var response = await _userService.CreateAsync(request);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] UserUpdateRequest request)
        {
            if (!request.IsValid())
                return HandleValidationErrors(request.ValidationResult);

            var user = await _userService.UpdateAsync(id, request);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _userService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
