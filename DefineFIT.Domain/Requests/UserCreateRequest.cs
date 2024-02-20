using DefineFIT.Domain.Requests.Validators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DefineFIT.Domain.Requests
{
    public class UserCreateRequest : RequestBase
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [DisplayName("CPF")]
        public string Cpf { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;

        [JsonIgnore]
        public string User { get; private set; } = null!;

        public void SetUser(string user)
        {
            User = user;
        }

        public override bool IsValid()
        {
            ValidationResult = new UserCreateRequestValidator()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
