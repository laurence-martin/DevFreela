using DevFreela.Application.Commands.CreateComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommentCommandHandlerTests
    {
        [Fact]
        public async Task AValidInputData_Executed_ReturnCommentId()
        {
            //Arrange
            var mock = new Mock<IProjectRepository>();
            var createCommentCommand = new CreateProjectCommentCommand("Comentário", 1, 1);
            var createCommentCommandHandler = new CreateProjectCommentCommandHandler(mock.Object);

            //Act
            var id = await createCommentCommandHandler.Handle(createCommentCommand, new CancellationToken());

            //Assert
            Assert.True(id >= 0);
            mock.Verify(p => p.CreateCommentAsync(It.IsAny<ProjectComment>()), Times.Once);
        }
    }
}
