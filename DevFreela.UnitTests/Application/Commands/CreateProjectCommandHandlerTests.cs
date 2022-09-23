using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task ReceivedCreateProject_Executed_ReturnProjectId()
        {
            //Arrange
            var mock = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand("Projeto teste", "Descrição Projeto Teste", 1, 2, 10000);
            var createProjectCommandHandler = new CreateProjectCommandHandler(mock.Object);
            
            //Act
            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            //Assert
            Assert.True(id >= 0);
            mock.Verify(p => p.CreateAsync(It.IsAny<Project>()), Times.Once());

        }
    }
}
