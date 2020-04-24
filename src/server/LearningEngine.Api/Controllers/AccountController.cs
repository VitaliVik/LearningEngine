using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearningEngine.Domain.Query;
using System.IdentityModel.Tokens.Jwt;
using LearningEngine.Api.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using LearningEngine.Api.ViewModels;
using LearningEngine.Domain.Command;

namespace LearningEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtTokenCryptographer _workWithJwtToken;
        public AccountController(IMediator mediator, IJwtTokenCryptographer workWithJwtToken)
        {
            _mediator = mediator;
            _workWithJwtToken = workWithJwtToken;
        }
        [HttpGet("token")]
        public IActionResult Token()
        {
            return Ok("bibus");
        }
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromForm]string username, [FromForm] string password)
        {
            var query = new GetIdentityQuery(username, password);
            var identity = await _mediator.Send(query);
            if (identity == null)
            {
                return BadRequest();
            }

            var encodedJwt = _workWithJwtToken.Encode(identity);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return new JsonResult(response);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm]RegisterViewModel vm)
        {
            var command = new RegisterUserCommand(vm.UserName, vm.Email, vm.Password);

            await _mediator.Send(command);

            return Ok();
        }
    }
}