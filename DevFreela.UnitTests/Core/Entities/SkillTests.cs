using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Core.Entities
{
    public class SkillTests
    {
        private readonly Skill _skill;
        public SkillTests()
        {
            _skill = new Skill("C#");
        }
        [Fact]
        public void SkillCreate_Executed_CreatedAtIsNow()
        {
            Assert.NotNull(_skill.CreatedAt);
        }

        [Fact]
        public void SkillUpdate_Execute_DescriptionUpdated()
        {
            _skill.Update("C# 10");
            Assert.Equal("C# 10", _skill.Description);
        }
    }
}
