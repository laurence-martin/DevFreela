using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Description)
                .MinimumLength(20)
                .WithMessage("Descrição do Projeto deve ter no mínimo 20 caracteres");
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Título do Projeto não pode ser nulo");
            RuleFor(x => x.TotalCost)
                .GreaterThan(0)
                .WithMessage("Custo total do Projeto deve ser maior que zero");
        }
    }
}
