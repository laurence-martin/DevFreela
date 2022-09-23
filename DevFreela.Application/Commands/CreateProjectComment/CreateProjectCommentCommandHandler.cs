using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistense;
using MediatR;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateProjectCommentCommandHandler : IRequestHandler<CreateProjectCommentCommand, int>
    {
        private readonly IProjectRepository _repository;
        public CreateProjectCommentCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(CreateProjectCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(
                            request.Content,
                            request.IdUser,
                            request.IdProject);
            await _repository.CreateCommentAsync(comment);
            
            return comment.Id;
        }
    }
}
