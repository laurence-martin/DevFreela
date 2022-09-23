using DevFreela.Application.Commands.UpdateSkill;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
    {
        public UpdateSkillCommandValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30)
                .WithMessage("Descrição deve conter entre 1 e 30 caracteres");
        }
    }
}
