using DefineFIT.Domain.Requests.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DefineFIT.Domain.Requests
{
    public class UserUpdateRequest : RequestBase
    {
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [DisplayName("CPF")]
        public string? Cpf { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        public DateTime? BirthDate { get; set; }

        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        public bool? Active { get; set; }

        [JsonIgnore]
        public string User { get; private set; } = null!;

        public void SetUser(string user)
        {
            User = user;
        }

        public override bool IsValid()
        {
            ValidationResult = new UserUpdateRequestValidator()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
