using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllComments
{
    public class GetAllCommentsByProjectHandler : IRequestHandler<GetAllCommentsByProjectQuery, List<CommentViewModel>>
    {
        private readonly IProjectRepository _repository;
        public GetAllCommentsByProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CommentViewModel>> Handle(GetAllCommentsByProjectQuery request, CancellationToken cancellationToken)
        {
            var comments = await _repository.GetAllCommentAsync(request.IdProject);

            return comments.Select(p => new CommentViewModel(p.Content,p.User.FullName)).ToList();
            
        }
    }
}
