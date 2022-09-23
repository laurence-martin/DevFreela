using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateProjectCommentCommand : IRequest<int>
    {
        public CreateProjectCommentCommand(string content, int idUser, int idProject)
        {
            Content = content;
            IdUser = idUser;
            IdProject = idProject;
        }

        public string Content { get; private set; }
        public int IdUser { get; private set; }
        public int IdProject { get; private set; }
    }
}
