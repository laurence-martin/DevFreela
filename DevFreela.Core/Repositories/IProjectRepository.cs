using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task CreateAsync(Project project);
        Task DeleteAsync(Project project);
        Task SaveChangesAsync();
        Task CreateCommentAsync(ProjectComment comment);

        Task<List<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(int id);
        Task<List<ProjectComment>> GetAllCommentAsync(int idProject);
        Task<ProjectComment> GetCommentByIdAsync(int idProject, int idComment);
    }
}
