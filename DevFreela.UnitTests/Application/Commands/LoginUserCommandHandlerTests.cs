using DevFreela.Application.Commands.LoginUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Services;
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
            mock.Setup(p => p.GetUserByEmailAndPasswordAsync("laurence_martin@outlook.com", "1234").Result).Returns(user);
            var authService = new AuthService();

            var loginCommand = new LoginUserCommand("laurence_martin@outlook.com", "1234");
            var loginCommandHandler = new LoginUserCommandHandler(authService, mock.Object);

            var userViewModel = await loginCommandHandler.Handle(loginCommand, new CancellationToken());

            Assert.NotNull(userViewModel);
            Assert.Equal("token_de_teste", userViewModel.Token);
            Assert.Equal("laurence_martin@outlook.com", userViewModel.Email);
            mock.Verify(p => p.GetUserByEmailAndPasswordAsync("laurence_martin@outlook.com", "1234").Result, Times.Once());
        }
    }
}
