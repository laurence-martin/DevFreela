using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateSkillCommandHandlerTests
    {
        [Fact]
        public async Task AValidInputData_Executed_ReturnSkillId()
        {
            //Arrange
            var mock = new Mock<ISkillRepository>();
            var createSkillCommand = new CreateSkillCommand("C#");
            var createSkillCommandHandler = new CreateSkillCommandHandler(mock.Object);
            //Act
            var id = await createSkillCommandHandler.Handle(createSkillCommand, new CancellationToken());
            //Assert
            Assert.True(id >= 0);
            mock.Verify(p => p.Create(It.IsAny<Skill>()), Times.Once());
        }
    }
}
