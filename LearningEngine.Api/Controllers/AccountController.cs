using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearningEngine.Application.Query;
using System.IdentityModel.Tokens.Jwt;
using LearningEngine.Api.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace LearningEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token(string username, string password)
        {
            var query = new GetIdentityQuery(username, password);
            var identity = await _mediator.Send(query);
            if (identity == null)
            {
                return BadRequest();
            }
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new Microsoft.IdentityModel.Tokens
                    .SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),SecurityAlgorithms.HmacSha256)
                );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return Ok(response);
        }

    }
}