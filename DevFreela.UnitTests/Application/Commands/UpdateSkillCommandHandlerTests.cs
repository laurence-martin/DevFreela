using DevFreela.Application.Commands.UpdateSkill;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class UpdateSkillCommandHandlerTests
    {
        [Fact]
        private async Task AValidInputData_Executed_UpdateSkillRecord()
        {
            var skill = new Skill("C#");
            var mock = new Mock<ISkillRepository>();
            mock.Setup(p => p.GetByIdAsync(1).Result).Returns(skill);
            var updateSkillCommand = new UpdateSkillCommand(1, "Backend");
            var updateSkillCommandHandler = new UpdateSkillCommandHandler(mock.Object);

            await updateSkillCommandHandler.Handle(updateSkillCommand, new CancellationToken());

            var updatedSkill =  await mock.Object.GetByIdAsync(1);

            Assert.NotNull(updatedSkill);
            Assert.Equal("Backend", updatedSkill.Description);

            mock.Verify(p => p.SaveChangesAsync(), Times.Once); 


        }
    }
}
