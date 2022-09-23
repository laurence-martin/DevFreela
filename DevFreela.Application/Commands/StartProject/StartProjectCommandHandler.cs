using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistense;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand>
    {
        private readonly IProjectRepository _repository;
        public StartProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Id);

            if (project != null)
            {
                project.StartProject();
                await _repository.SaveChangesAsync();
            }
            return Unit.Value;
        }
    }
}
