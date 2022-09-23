using DevFreela.Application.Commands.LoginUser;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace DevFreela.Application.Validators
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("E-mail inválido");
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Senha não pode ser nula");
        }
    }
}
