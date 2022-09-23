using DevFreela.Application.Commands.CreateSkill;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
    {
        public CreateSkillCommandValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30)
                .WithMessage("Descrição deve conter entre 1 e 30 caracteres");
        }
    }
}
