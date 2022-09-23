using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetCommentByCommentId
{
    public class GetCommentByCommentIdHandler : IRequestHandler<GetCommentByCommentIdQuery, ProjectComment>
    {
        private readonly IProjectRepository _repository;
        public GetCommentByCommentIdHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProjectComment> Handle(GetCommentByCommentIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetCommentByIdAsync(request.IdProject, request.IdComment);
        }
    }
}
