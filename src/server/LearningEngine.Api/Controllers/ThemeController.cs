using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearningEngine.Api.ViewModels;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Query;
using LearningEngine.Application.UseCase.Command;
using System.Net.WebSockets;
using LearningEngine.Api.Authorization;
using LearningEngine.Api.Extensions;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.DTO;
using LearningEngine.Application.UseCase.Query;

namespace LearningEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtTokenCryptographer _workWithJwtToken;

        public ThemeController(IMediator mediator, IJwtTokenCryptographer workWithJwtToken)
        {
            _mediator = mediator;
            _workWithJwtToken = workWithJwtToken;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateUserTheme([FromForm]CreateThemeViewModel vm)
        {
            var command = new CreateUserThemeCommand(this.GetUserName(), vm.ThemeName, vm.Description, 
                                                     vm.IsPublic, this.GetUserId(), vm.ParentThemeId);

            try
            {
                await _mediator.Send(command);

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("{themeId}")]
        public async Task<IActionResult> DeleteTheme([FromRoute] int themeId)
        {
            var command = new DeleteThemeCommand(themeId, this.GetUserId());

            try
            {
                await _mediator.Send(command);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{themeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTheme([FromRoute] int themeId)
        {
            var query = new GetThemeHeaderQuery(themeId);

            try
            {
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        
        [HttpGet("{themeId}/fullInfo")]
        public async Task<IActionResult> GetFullInfo([FromRoute] int themeId)
        {
            var query = new GetThemeFullInfoQuery(this.GetUserId(), themeId);

            try
            {
                var result = await _mediator.Send(query);

                return Ok(new { theme = result, isRoot = false });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("userRootThemes")]
        public async Task<IActionResult> GetUserRootThemes()
        {
            var query = new GetRootThemesByUserIdQuery(this.GetUserId());

            try
            {
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("linkUserToTheme")]
        public async Task<IActionResult> LinkUserToTheme([FromForm]int themeId, [FromForm]TypeAccess typeAccess)
        {
            var command = new LinkThemeAndAllSubThemesToUserCommand(this.GetUserId(), themeId, typeAccess);

            try
            {
                var result = await _mediator.Send(command);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{themeId}")]
        public async Task<IActionResult> EditTheme([FromForm]ThemeDto themeDto, [FromRoute]int themeId)
        {
            var command = new EditThemeCommand(themeDto, this.GetUserId(), themeId);

            try
            {
                await _mediator.Send(command);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}