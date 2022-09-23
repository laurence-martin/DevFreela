using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Queries.GetAllComments
{
    public class GetAllCommentsByProjectQuery : IRequest<List<CommentViewModel>>
    {
        public GetAllCommentsByProjectQuery(int idProject)
        {
            IdProject = idProject;
        }

        public int IdProject { get; private set; }
    }
}
