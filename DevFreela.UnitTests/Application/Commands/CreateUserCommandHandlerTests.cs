using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
       
        [Fact]
        public async Task AValidInputData_Execute_ReturnUserId() 
        {
            //Arrange
            var mockAuth = new Mock<IAuthService>();
            mockAuth.Setup(p => p.ComputeSha256Hash("1234")).Returns("password");
            var mock = new Mock<IUserRepository>();
            var createUserCommand = new CreateUserCommand("Laurence Martin", "laurence_martin@outlook.com", "1234", new DateTime(1997, 12, 15), UserRoleEnum.Client);
            var createUserCommandHandler = new CreateUserCommandHandler(mock.Object, mockAuth.Object);
            //Act
            var id = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());
            //Assert
            Assert.True(id >= 0);
            mock.Verify(p => p.Create(It.IsAny<User>()), Times.Once());
        }
    }
}
