using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllSkillsCommandTests
    {
        [Fact]
        public async Task TwoSkills_Executed_ReturnTwoSkillViewModel()
        {
            //Arrange
            var skillList = new List<Skill>{
                new Skill("C#"),
                new Skill("Backend") };

            var skillMock = new Mock<ISkillRepository>();
            skillMock.Setup(x => x.GetAllAsync().Result).Returns(skillList);

            var skillQuery = new GetAllSkillsQuery();
            var skillHandler = new GetAllSkillsHandler(skillMock.Object);

            //Act
            var skillViewModel = await skillHandler.Handle(skillQuery, new CancellationToken());
            
            //Assert
            Assert.NotNull(skillViewModel);
            Assert.NotEmpty(skillViewModel);
            Assert.Equal(skillList.Count, skillViewModel.Count);

            skillMock.Verify(p => p.GetAllAsync().Result, Times.Once());

        }
    }
}
