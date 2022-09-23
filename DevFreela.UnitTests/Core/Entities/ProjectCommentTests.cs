using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectCommentTests
    {
        [Fact]
        public void CommentCreated_Execute_CreatedIsNotNull()
        {
            var projectComment = new ProjectComment("Comment", 1, 1);
            Assert.NotNull(projectComment.CreatedAt);
        }
    }
}
