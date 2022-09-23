using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistense;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetSkillById
{
    public class GetSkillByIdHandler : IRequestHandler<GetSkillByIdQuery, SkillViewModel>
    {
        private readonly ISkillRepository _repository;
        public GetSkillByIdHandler(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<SkillViewModel> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            var skill = await _repository.GetByIdAsync(request.Id);

            if (skill != null)
                return new SkillViewModel(skill.Id, skill.Description);

            return null;
        }
    }
}
