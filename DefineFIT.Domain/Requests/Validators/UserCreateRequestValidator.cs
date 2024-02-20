using DefineFIT.Domain.Requests;
using FluentValidation;

namespace DefineFIT.Domain.Requests.Validators
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty()
                .WithMessage("Nome não pode ser vazio.")
                .Length(3, 50)
                .WithMessage("O nome deve conter no mínimo 3 caracteres e no maximo 100 caracteres.");

            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email não pode ser vazio.")
                .EmailAddress()
                .WithMessage("Email não é válido.");

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Senha não pode ser vazio.")
                .MinimumLength(8)
                .WithMessage("Senha deve ter no mínimo 8 caracteres.")
                .MaximumLength(50)
                .WithMessage("Senha deve ter no máximo 50 caracteres.");

            RuleFor(user => user.BirthDate)
                .NotEmpty()
                .WithMessage("Data de nascimento não pode ser vazio.")
                .LessThan(DateTime.Now)
                .WithMessage("Data de nascimento não pode ser maior que a data atual.");

            RuleFor(user => user.Cpf)
                .NotEmpty()
                .WithMessage("CPF não pode ser vazio.")
                .Length(11)
                .WithMessage("CPF inválido.");

            RuleFor(user => user.PhoneNumber)
                .NotEmpty()
                .WithMessage("Telefone não pode ser vazio.")
                .Length(11)
                .WithMessage("Telefone inválido.");
        }
    }
}
