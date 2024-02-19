using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DefineFIT.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleValidationErrors(FluentValidation.Results.ValidationResult validationResult)
        {
            var errors = validationResult.Errors
                .Select(error => new { error.PropertyName, error.ErrorMessage })
                .ToList();

            return BadRequest(errors);
        }
    }
}
