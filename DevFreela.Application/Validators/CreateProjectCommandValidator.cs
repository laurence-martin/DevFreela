using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
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
