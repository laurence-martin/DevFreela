using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Queries.GetUserById;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateUserCommand> _createUserCommandValidator;
        private readonly IValidator<LoginUserCommand> _loginUserCommandValidator;
        public UserController(
            IMediator mediator, 
            IValidator<CreateUserCommand> createUserCommandValidator,
            IValidator<LoginUserCommand> loginUserCommandValidator)
        {
            _createUserCommandValidator = createUserCommandValidator;
            _mediator = mediator;
            _loginUserCommandValidator = loginUserCommandValidator;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetUserByIdQuery(id);

            var user  = await _mediator.Send(query);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var isValid = await _createUserCommandValidator.ValidateAsync(command);
            if (!isValid.IsValid)
            {
                var messages = isValid.Errors
                                    .Select(x => x.ErrorMessage)
                                    .ToList();
                return BadRequest(messages);
            }
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new {id = id}, command);
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody ] LoginUserCommand command)
        {
            var isValid = await _loginUserCommandValidator.ValidateAsync(command);
            if (!isValid.IsValid)
            {
                var messages = isValid.Errors
                                    .Select(x => x.ErrorMessage)
                                    .ToList();
                return BadRequest(messages);
            }

            var userLoginViewModel = await _mediator.Send(command);
            if (userLoginViewModel == null)
                return BadRequest("Login incorreto");

            return Ok(userLoginViewModel);
        }
    }
}
