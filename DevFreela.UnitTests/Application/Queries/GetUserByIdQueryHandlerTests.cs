
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetUserByIdQueryHandlerTests
    {
        [Fact]
        public async Task AUserId_Executed_ReturnUserViewModel()
        {
            //Arrange
            var user = new User("Laurence Martin", "laurence_martin@outlook.com", new DateTime(1977, 12, 15), "2123123", DevFreela.Core.Enums.UserRoleEnum.Client);

            var userMock = new Mock<IUserRepository>();
            userMock.Setup(p => p.GetByIdAsync(1).Result).Returns(user);

            var userQuery = new GetUserByIdQuery(1);
            var userHandle = new GetUserByIdHandler(userMock.Object);

            //Act
            var userViewModel = await userHandle.Handle(userQuery, new CancellationToken());

            //Assert
            Assert.NotNull(userViewModel);
            userMock.Verify(p => p.GetByIdAsync(1).Result, Times.Once);
        }
    }
}
