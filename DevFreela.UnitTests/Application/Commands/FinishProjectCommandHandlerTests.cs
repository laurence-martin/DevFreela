using DevFreela.Application.Commands.FinishProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class FinishProjectCommandHandlerTests
    {
        [Fact]
        public async Task AProjectId_Execute_UpdateProjectToFinished()
        {
            //Arrange
            var project = new Project("projeto teste", "teste", 1, 1, 10000);
            project.StartProject();

            var mock = new Mock<IProjectRepository>();
            mock.Setup(p => p.GetByIdAsync(1).Result).Returns(project);

            
            var finishCommand = new FinishProjectCommand(1);
            var finishCommandHandler = new FinishProjectCommandHandler(mock.Object);

            //Act
            await finishCommandHandler.Handle(finishCommand, new CancellationToken());
            var finishedProject = await mock.Object.GetByIdAsync(1);

            //Assert
            Assert.NotNull(finishedProject);
            Assert.Equal(ProjectStatusEnum.Finished, finishedProject.Status);
            mock.Verify(p => p.SaveChangesAsync(), Times.Once);

        }
    }
}
