using MediatR;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommand : IRequest
    {
        public FinishProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
