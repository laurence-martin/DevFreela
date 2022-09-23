using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<List<ProjectViewModel>>
    {
        private readonly string _query;
        public GetAllProjectsQuery(string query)
        {
            _query = query;
        }
    }
}
