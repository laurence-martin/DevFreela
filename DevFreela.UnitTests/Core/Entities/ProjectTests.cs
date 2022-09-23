using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTests
    {
        private readonly Project _project;
        public ProjectTests()
        {
            _project = new Project(
                "Projeto de Testes",
                "Projeto de Testes",
                1,
                2,
                10000M);
        }
        [Fact]
        public void Project_Create_ProjectStatusIsCreatedAndProjectStartedIsNull()
        {
            Assert.Equal(ProjectStatusEnum.Created, _project.Status);
            Assert.Null(_project.StartedAt);
        }

        [Fact]
        public void ProjectStarts_Executed_ProjectStatusIsInProgressAndProjectStartedAtIsNow()
        {
            _project.StartProject();
            Assert.Equal(ProjectStatusEnum.InProgress, _project.Status);
            Assert.NotNull(_project.StartedAt);
        }
        [Fact]
        public void ProjectFinish_Executed_ProjectStatusIsFinishedAndProjectFinishedAtIsNow()
        {
            _project.StartProject();
            
            _project.FinishProject();
            Assert.Equal(ProjectStatusEnum.Finished, _project.Status);
            Assert.NotNull(_project.FinishedAt);
        }
        [Fact]
        public void ProjectCancel_Executed_ProjectStatusIsCancelled()
        {
            _project.StartProject();
            _project.CancelProject();
            Assert.Equal(ProjectStatusEnum.Cancelled, _project.Status);
        }
        [Fact]
        public void ProjectSuspend_Executed_ProjectStatusIsSuspended()
        {
            _project.StartProject();
            _project.SuspendProject();
            Assert.Equal(ProjectStatusEnum.Suspended, _project.Status);
        }
        [Fact]
        public void ProjectUnsuspend_Executed_ProjectStatusIsInProgress()
        {
            _project.StartProject();
            _project.SuspendProject();
            _project.UnsuspendProject();
            Assert.Equal(ProjectStatusEnum.InProgress, _project.Status);
        }
        [Fact]
        public void ProjectDataUpdated_Executed_ProjectReflectsUpdate()
        {
            _project.Update("NewTittle", "NewDescription", 20000);
            Assert.Equal(_project.Title, "NewTittle");
            Assert.Equal(_project.Description, "NewDescription");
            Assert.Equal(_project.TotalCost, 20000);
        }
    }
}
