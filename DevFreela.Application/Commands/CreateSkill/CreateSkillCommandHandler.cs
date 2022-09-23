using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infra.Persistense;
using MediatR;

namespace DevFreela.Application.Commands.CreateSkill
{
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, int>
    {
        private readonly ISkillRepository _repository;
        public CreateSkillCommandHandler(ISkillRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill(request.Description);
            await _repository.Create(skill);

            return skill.Id;
        }
    }
}
