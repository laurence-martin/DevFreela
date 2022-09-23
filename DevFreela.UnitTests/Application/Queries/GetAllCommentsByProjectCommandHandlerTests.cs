using DevFreela.Application.Queries.GetAllComments;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllCommentsByProjectCommandHandlerTests
    {
        private readonly List<ProjectComment> _commentList;
        public GetAllCommentsByProjectCommandHandlerTests()
        {
            //_user = new User("Laurence Martin", "laurence_martin@outlook.com", new DateTime(1977, 12, 15), "1231", 0);
            _commentList = new List<ProjectComment>
                {
                    new ProjectComment("Comentário 1", 1, 1),
                    new ProjectComment("Comentário 2", 1, 1),
                    new ProjectComment("Comentário 3", 1, 1)
                };
            foreach(var comment in _commentList)
            {
                comment.User = new User("Laurence Martin", "laurence_martin@outlook.com", new DateTime(1977, 12, 15), "12312313", UserRoleEnum.Client);
            }
        }
        [Fact]
        public async Task ThreeCommentsExists_Executed_ReturnThreeComments()
        {
            //Arrange
                //Setup do MOQ
            
            var commentsMock = new Mock<IProjectRepository>();
            commentsMock.Setup(p => p.GetAllCommentAsync(1).Result).Returns(_commentList);

            var commentsQuery = new GetAllCommentsByProjectQuery(1);                
            var commentsHandler = new GetAllCommentsByProjectHandler(commentsMock.Object);

            //Act
            var commentsViewModel = await commentsHandler.Handle(commentsQuery, new CancellationToken());

            //Assert
            Assert.NotNull(commentsViewModel);
            Assert.NotEmpty(commentsViewModel);
            Assert.Equal(_commentList.Count, commentsViewModel.Count);

            commentsMock.Verify(p => p.GetAllCommentAsync(1).Result, Times.Once());

        }
    }
}
