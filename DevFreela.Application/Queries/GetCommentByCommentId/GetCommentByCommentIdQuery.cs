using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Queries.GetCommentByCommentId
{
    public class GetCommentByCommentIdQuery : IRequest<ProjectComment>
    {
        public GetCommentByCommentIdQuery(int idProject, int idComment)
        {
            IdProject = idProject;
            IdComment = idComment;
        }

        public int IdProject { get; private set; }
        public int IdComment { get; private set; }
    }
}
