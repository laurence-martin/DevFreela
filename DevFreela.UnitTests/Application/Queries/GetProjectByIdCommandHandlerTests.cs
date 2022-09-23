using DevFreela.Application.Queries.GetAllComments;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetProjectByIdCommandHandlerTests
    {
        private readonly Project _project;
        public GetProjectByIdCommandHandlerTests()
        {
            _project = new Project("Projeto teste", "Desc projeto teste", 1, 2, 10000);
            _project.Client = new User("Laurence Martin", "laurence_martin@outlook.com",  new DateTime(1977, 12, 15), "12312313", UserRoleEnum.Client);
            _project.Freelancer = new User("Jose Renato Martin", "joserenato_martin@outlook.com",  new DateTime(1977, 12, 15), "12312313", UserRoleEnum.Client);

        }
        [Fact]
        public async Task AProjectId_Executed_ReturnProjectViewModel()
        {
            //Arrange
            //Setup do MOQ

            var projectsMock = new Mock<IProjectRepository>();
            projectsMock.Setup(p => p.GetByIdAsync(1).Result).Returns(_project);

            var projectsQuery = new GetProjectByIdQuery(1);
            var projectsHandler = new GetProjectByIdHandler(projectsMock.Object);

            //Act
            var projectViewModel = await projectsHandler.Handle(projectsQuery, new CancellationToken());

            //Assert
            Assert.NotNull(projectViewModel);
           
           
            projectsMock.Verify(p => p.GetByIdAsync(1).Result, Times.Once());

        }
    }
}
