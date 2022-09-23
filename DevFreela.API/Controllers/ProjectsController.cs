using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Queries.GetAllComments;
using DevFreela.Application.Queries.GetCommentByCommentId;
using FluentValidation;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateProjectCommand> _createProjectValidator;
        private readonly IValidator<UpdateProjectCommand> _updateProjectValidator;
        private readonly IValidator<CreateProjectCommentCommand> _createProjectCommentValidator;

        public ProjectsController(IMediator mediator, 
            IValidator<CreateProjectCommand> createProjectValidator,
            IValidator<UpdateProjectCommand> updateProjectValidator,
            IValidator<CreateProjectCommentCommand> createProjectCommentValidator)
        {
            _mediator = mediator;
            _createProjectValidator = createProjectValidator;
            _updateProjectValidator = updateProjectValidator;
            _createProjectCommentValidator = createProjectCommentValidator;
        }
        [HttpGet]
        [Authorize(Roles ="0,1")]
        public async Task<IActionResult> Get(string query)
        {
            var get = new GetAllProjectsQuery(query);
            var projects = await _mediator.Send(get);
            return Ok(projects);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "0, 1")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProjectByIdQuery(id);
            var projectDetail = await _mediator.Send(query);
            
            if (projectDetail == null)
               return NotFound();
            
            return Ok(projectDetail);
        }
        
        [HttpPost]
        [Authorize(Roles = "0")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            var isValid = await _createProjectValidator.ValidateAsync(command); 
            if (!isValid.IsValid)
            {
                var messages = isValid.Errors
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return BadRequest(messages);
            }
            
            var id = await _mediator.Send(command);
            
            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "0")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            var isValid = await _updateProjectValidator.ValidateAsync(command);
            if (!isValid.IsValid)
            {
                var messages = isValid.Errors
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return BadRequest(messages);
            }
            
            command.SetId(id);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "0")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
        
        [HttpPut("{id:int}/start")]
        [Authorize(Roles = "0")]
        public async Task<IActionResult> Start(int id)
        {
            var command = new StartProjectCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id:int}/finish")]
        [Authorize(Roles = "0")]
        public async Task<IActionResult> Finish(int id)
        {
            var command = new FinishProjectCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{id:int}/comments")]
        [Authorize(Roles = "0,1")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateProjectCommentCommand command)
        {
            var isValid = await _createProjectCommentValidator.ValidateAsync(command);
            if (!isValid.IsValid)
            {
                var messages = isValid.Errors
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return BadRequest(messages);
            }
            var idcomment = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCommentById), new { idproject = id, idcomment = idcomment }, command);
        }

        [HttpGet("{id:int}/comments")]
        [Authorize(Roles = "0,1")]
        public async Task<IActionResult> GetCommentsByProject(int id)
        {
            var get = new GetAllCommentsByProjectQuery(id);
            var comments = await _mediator.Send(get);
            return Ok(comments);
        }

        [HttpGet("{idproject:int}/comments/{idcomment:int}")]
        [Authorize(Roles = "0,1")]
        public async Task<IActionResult> GetCommentById(int idproject, int idcomment)
        {
            var get = new GetCommentByCommentIdQuery(idproject, idcomment);
            var comment = await _mediator.Send(get);
            return Ok(comment);
        }
    }
}
