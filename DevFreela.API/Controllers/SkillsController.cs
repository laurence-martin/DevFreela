using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Commands.UpdateSkill;
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Queries.GetSkillById;
using DevFreela.Application.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    [Authorize]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateSkillCommand> _createSkillValidator;
        private readonly IValidator<UpdateSkillCommand> _updateSkillValidator;
        public SkillsController(IMediator mediator, 
            IValidator<UpdateSkillCommand> updateSkillValidator,
            IValidator<CreateSkillCommand> createSkillValidator)
        {
            _mediator = mediator;
            _updateSkillValidator = updateSkillValidator;
            _createSkillValidator = createSkillValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSkillsQuery();
            var skills = await _mediator.Send(query);
            
            return Ok(skills);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetSkillByIdQuery(id);
            var skill = await _mediator.Send(query);
            if (skill == null)
                return NotFound();

            return Ok(skill);
        }

        [HttpPost]
        [Authorize(Roles ="0")]
        public async Task<IActionResult> Post([FromBody] CreateSkillCommand command)
        {
            var isValid = await _createSkillValidator.ValidateAsync(command);
            if (!isValid.IsValid)
            {
                var messages = isValid.Errors
                                    .Select(x => x.ErrorMessage)
                                    .ToList();
                return BadRequest(messages);
            }

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles ="0")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateSkillCommand command)
        {
            var isValid = await _updateSkillValidator.ValidateAsync(command);
            if (!isValid.IsValid)
            {
                var messages = isValid.Errors
                                    .Select(x => x.ErrorMessage)
                                    .ToList();
                return BadRequest(messages);
            }
            command.SetId(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
