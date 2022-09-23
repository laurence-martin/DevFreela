using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistense;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>>
    {
        private readonly ISkillRepository _repository;
        public GetAllSkillsHandler(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await _repository.GetAllAsync();
            var skillsViewModel = skills
                            .Select(p => new SkillViewModel(p.Id, p.Description))
                            .ToList();
            return skillsViewModel;
        }
    }
}
