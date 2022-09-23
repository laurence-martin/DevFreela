using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistense;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand>
    {
        private readonly IProjectRepository _repository;
        public FinishProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Id);
            if (project != null)
            {
                project.FinishProject();
                await _repository.SaveChangesAsync();
            }
            return Unit.Value;

        }
    }
}
