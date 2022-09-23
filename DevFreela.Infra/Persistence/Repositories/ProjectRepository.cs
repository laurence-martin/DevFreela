using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistense;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infra.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }
        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbContext.Projects
                            .Include(p => p.Client)
                            .Include(p => p.Freelancer)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<ProjectComment>> GetAllCommentAsync(int idProject)
        {
            return _dbContext.Comments
                    .Where(c => c.IdProject == idProject)
                    .Include(c => c.User)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<ProjectComment> GetCommentByIdAsync(int idProject, int idComment)
        {
            return await _dbContext.Comments
                .Where(p => p.IdProject == idProject && p.Id == idComment)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Project project)
        {
            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateCommentAsync(ProjectComment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}