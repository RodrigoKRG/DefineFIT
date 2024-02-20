using DefineFIT.Domain.Repositories;
using DefineFIT.Domain.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineFIT.Domain.Validators
{
    public class UserCreateValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateValidator(IUserRepository userRepository)
        {
               RuleFor(x => x.Cpf)
                .NotEmpty()
                .WithMessage("CPF is required.")
                .MustAsync(async (cpf, cancellation) => !await userRepository.ExistsByCpfAsync(cpf))
                .WithMessage("CPF já cadastrado.");
        }
    }
}
