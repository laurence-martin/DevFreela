using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Services;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
       
        [Fact]
        public async Task AValidInputData_Execute_ReturnUserId() 
        {
            //Arrange
            var authService = new AuthService();
            
            var mock = new Mock<IUserRepository>();
            var createUserCommand = new CreateUserCommand("Laurence Martin", "laurence_martin@outlook.com", "213123", new DateTime(1997, 12, 15), UserRoleEnum.Client);
            var createUserCommandHandler = new CreateUserCommandHandler(mock.Object, authService);
            //Act
            var id = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());
            //Assert
            Assert.True(id >= 0);
            mock.Verify(p => p.Create(It.IsAny<User>()), Times.Once());
        }
    }
}
