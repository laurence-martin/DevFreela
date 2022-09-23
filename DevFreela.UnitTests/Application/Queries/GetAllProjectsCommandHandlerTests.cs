using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        private readonly List<Project> _projectList;
        public GetAllProjectsCommandHandlerTests()
        {
            _projectList = new List<Project>
            {
                new Project("Primeiro Projeto", "Desc Primeiro Projeto", 1, 2, 10000),
                new Project("Segundo Projeto", "Desc Segundo Projeto", 1, 2, 20000),
                new Project("Terceiro Projeto", "Desc Terceiro Projeto", 1, 2, 30000)
            };
        }
        [Fact]
        public async Task ThreeProjectsExists_Executed_ReturnThreeProjectViewModels()
        {
            //Arragen
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(p => p.GetAllAsync().Result).Returns(_projectList);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var getAllProjectsHandler = new GetAllProjectsHandler(projectRepositoryMock.Object);

            //Act
            var projectViewModel = await getAllProjectsHandler.Handle(getAllProjectsQuery, new CancellationToken());

            //Assert
            Assert.NotNull(projectViewModel);
            Assert.NotEmpty(projectViewModel);
            Assert.Equal(_projectList.Count, projectViewModel.Count);

            projectRepositoryMock.Verify(p => p.GetAllAsync().Result, Times.Once());

        }
    }
}
