using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistense;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infra.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        public SkillRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Skill skill)
        {
            await _dbContext.Skills.AddAsync(skill);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Skill>> GetAllAsync()
        {
            return await _dbContext.Skills
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            return await _dbContext.Skills.SingleOrDefaultAsync(p => p.Id == id);
        }

        
    }
}
