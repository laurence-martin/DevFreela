using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
    {
        private readonly IProjectRepository _repository;
        public GetProjectByIdHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Id);
            if (project == null)
                return null;

            var projectDetailsViewModel = new ProjectDetailsViewModel(
                project.Id,
                project.Title,
                project.Description,
                project.TotalCost,
                project.CreateAt,
                project.StartedAt,
                project.FinishedAt,
                project.Status,
                project.Client.FullName,
                project.Freelancer.FullName);
            return projectDetailsViewModel;


        }
    }
}
