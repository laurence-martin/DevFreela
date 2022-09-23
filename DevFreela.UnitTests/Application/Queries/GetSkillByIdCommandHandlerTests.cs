
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Queries.GetSkillById;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetSkillByIdCommandHandlerTests
    {
        [Fact]
        public async Task ASkillId_Executed_ReturnSkillViewModel()
        {
            //Arrange
            var skill = new Skill("C#");
            var skillMock = new Mock<ISkillRepository>();
            skillMock.Setup(x => x.GetByIdAsync(1).Result).Returns(skill);

            var skillQuery = new GetSkillByIdQuery(1);
            var skillHandler = new GetSkillByIdHandler(skillMock.Object);
            //Act
            var skillViewModel = await skillHandler.Handle(skillQuery, new CancellationToken());

            //Assert
            Assert.NotNull(skillViewModel);
            skillMock.Verify(p => p.GetByIdAsync(1).Result, Times.Once);
        }
    }
}
