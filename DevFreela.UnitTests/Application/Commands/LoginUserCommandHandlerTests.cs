using DevFreela.Application.Commands.LoginUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using Moq;

namespace DevFreela.UnitTests.Application.Commands
{
    public class LoginUserCommandHandlerTests
    {
        [Fact]
        public async Task AValidUserAndPassword_Executed_ReturnsLoginUserViewModel()
        {
            var user = new User("Laurence Martin", "laurence_martin@outlook.com", new DateTime(1977, 12, 15), "1234", UserRoleEnum.Client);
            var mock = new Mock<IUserRepository>();
            mock.Setup(p => p.GetUserByEmailAndPasswordAsync("laurence_martin@outlook.com", "password").Result).Returns(user);
            var mockAuth = new Mock<IAuthService>();
            mockAuth.Setup(p => p.ComputeSha256Hash("1234")).Returns("password");
            mockAuth.Setup(p => p.GenerateJwtToken("laurence_martin@outlook.com", UserRoleEnum.Client)).Returns("token_de_teste");

            var loginCommand = new LoginUserCommand("laurence_martin@outlook.com", "1234");
            var loginCommandHandler = new LoginUserCommandHandler(mockAuth.Object, mock.Object);

            var userViewModel = await loginCommandHandler.Handle(loginCommand, new CancellationToken());

            Assert.NotNull(userViewModel);
            Assert.Equal("token_de_teste", userViewModel.Token);
            Assert.Equal("laurence_martin@outlook.com", userViewModel.Email);
            mock.Verify(p => p.GetUserByEmailAndPasswordAsync("laurence_martin@outlook.com", "password").Result, Times.Once());
        }
    }
}
