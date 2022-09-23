using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Commands.UpdateSkill;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Commands
{
    public class UpdateProjectCommandHandlerTests
    {
        [Fact]
        private async Task AValidInputData_Executed_UpdateProjectRecord()
        {
            var project = new Project("projeto antes do update", "projeto", 1, 2, 10000);
            var mock = new Mock<IProjectRepository>();
            mock.Setup(p => p.GetByIdAsync(1).Result).Returns(project);

            var updateProjectCommand = new UpdateProjectCommand(1, "projeto alterado", "projeto alterado", 20000);
            var updateProjectCommandHandler = new UpdateProjectCommandHandler(mock.Object);

            await updateProjectCommandHandler.Handle(updateProjectCommand, new CancellationToken());

            var updatedProject = await mock.Object.GetByIdAsync(1);

            Assert.NotNull(updatedProject);
            Assert.Equal("projeto alterado", updatedProject.Title);
            Assert.Equal("projeto alterado", updatedProject.Description);
            Assert.Equal(20000, updatedProject.TotalCost);

            mock.Verify(p => p.SaveChangesAsync(), Times.Once);
        }
    }
}
