using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Enums;
using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace DevFreela.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("É necessário um e-mail válido");


            RuleFor(x => x.Role)
                .Must(IsValidRole)
                .WithMessage("Tipo de usuário inválido");
            RuleFor(x => x.Password)
                .Must(IsValidPassword)
                .WithMessage("Senha deve contém no mínimo 8 caracteres, 1 letra maiúscula, 1 minúscula, 1 número e 1 caractere especial");
            RuleFor(x => x.FullName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10)
                .WithMessage("Nome não pode ser nulo e deve conter no mínimo 20 caracteres");
        }

        public bool IsValidPassword(string password)
        {
            var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            return regex.IsMatch(password);
        }

        public bool IsValidRole(UserRoleEnum role)
        {
            return (Enum.IsDefined(typeof(UserRoleEnum), role));
        }
    }
}
