using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class DeleteProjectCommandHandlerTests
    {
        [Fact]
        public async Task AProjectId_Execute_DeleteProject()
        {
            //Arrange
            var project = new Project("teste", "teste", 1, 1, 1111);
            var mock = new Mock<IProjectRepository>();
            mock.Setup(p => p.GetByIdAsync(1).Result).Returns(project);
            
            var deleteProjectCommand = new DeleteProjectCommand(1);
            var deleteProjectCommandHandler = new DeleteProjectCommandHandler(mock.Object);

            
            //Act
            await deleteProjectCommandHandler.Handle(deleteProjectCommand, new CancellationToken());

            //Assert
            mock.Verify(p => p.DeleteAsync(It.IsAny<Project>()), Times.Once());
            
        }
    }
}
