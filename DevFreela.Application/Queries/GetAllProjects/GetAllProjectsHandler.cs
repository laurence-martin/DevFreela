using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistense;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
    {
        private readonly IProjectRepository _repository;
        public GetAllProjectsHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAllAsync();
            var projectsViewModel = projects
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreateAt))
                .ToList();
            return projectsViewModel;
        }
    }
}
