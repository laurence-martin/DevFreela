using MediatR;

namespace DevFreela.Application.Commands.CreateSkill
{
    public class CreateSkillCommand : IRequest<int>
    {
        public CreateSkillCommand(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
    }
}
