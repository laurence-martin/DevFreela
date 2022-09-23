using DevFreela.Application.Commands.StartProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class StartProjectCommandHandlerTests
    {
        [Fact]
        public async Task AProjectId_Execute_UpdateProjectToStarted()
        {
            //Arrange
            var project = new Project("projeto teste", "teste", 1, 1, 10000);
            var mock = new Mock<IProjectRepository>();
            mock.Setup(p => p.GetByIdAsync(1).Result).Returns(project);
            
            var startCommand = new StartProjectCommand(1);
            var startCommandHandler = new StartProjectCommandHandler(mock.Object);

            await startCommandHandler.Handle(startCommand, new CancellationToken());

            var startedProject = await mock.Object.GetByIdAsync(1);

            Assert.NotNull(startedProject);
            Assert.Equal(ProjectStatusEnum.InProgress, startedProject.Status);
            mock.Verify(p => p.SaveChangesAsync(), Times.Once);
        }
    }
}
