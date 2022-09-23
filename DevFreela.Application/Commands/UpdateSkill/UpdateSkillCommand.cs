using MediatR;

namespace DevFreela.Application.Commands.UpdateSkill
{
    public class UpdateSkillCommand : IRequest<Unit>
    {
        public UpdateSkillCommand(int id, string description)
        {
            Id = id;
            Description = description;
        }
        public int Id { get; private set; }
        public string Description { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
