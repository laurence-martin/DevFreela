using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Core.Entities
{
    public class UserTests
    {
        [Fact]
        public void UserCreate_Executed_ActiveIsTrueAndCreatedAtIsNotNull()
        {
            var user = new User(
                "Laurence Martin",
                "laurence_martin@outlook.com",
                new DateTime(1977, 12, 15),
                "teste",
                UserRoleEnum.Client);
            Assert.True(user.Active);
            Assert.NotNull(user.CreatedAt);
        }
    }
}
