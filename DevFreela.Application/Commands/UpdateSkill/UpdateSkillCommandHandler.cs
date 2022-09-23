using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.UpdateSkill
{
    public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand>
    {
        private readonly ISkillRepository _repository;
        public UpdateSkillCommandHandler(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await _repository.GetByIdAsync(request.Id);
            
            if (skill != null)
            {
                skill.Update(request.Description);
            }
            await _repository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
