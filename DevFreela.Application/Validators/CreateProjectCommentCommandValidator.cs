using DevFreela.Application.Commands.CreateComment;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommentCommandValidator : AbstractValidator<CreateProjectCommentCommand>
    {
        public CreateProjectCommentCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .NotNull()
                .MinimumLength(10)
                .WithMessage("Conteúdo da mensagem deve ter pelo menos 10 caracteres");
        }
    }
}
