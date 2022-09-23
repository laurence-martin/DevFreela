using DevFreela.Application.Queries.GetCommentByCommentId;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetCommentByCommentIdCommandTests
    {
        [Fact]
        public async Task AComment_Executed_ReturnCommentViewModel()
        {
            //Arrange
            var comment = new ProjectComment("Teste", 1, 1);

            var commentMock = new Mock<IProjectRepository>();
            commentMock.Setup(x => x.GetCommentByIdAsync(1,1).Result).Returns(comment);

            var commentQuery = new GetCommentByCommentIdQuery(1, 1);
            var commentHandler = new GetCommentByCommentIdHandler(commentMock.Object);

            //Act
            var commentModel = await commentHandler.Handle(commentQuery, new CancellationToken());

            //Assert
            Assert.NotNull(commentModel);
            Assert.Equal(comment.Content, commentModel.Content);

            commentMock.Verify(p => p.GetCommentByIdAsync(1,1).Result, Times.Once());

        }
    }
}
