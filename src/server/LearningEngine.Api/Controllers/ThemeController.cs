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
            var command = new CreateUserThemeCommand(this.GetUserName(), vm.ThemeName, vm.Description, vm.IsPublic, vm.ParentThemeId);

            await _mediator.Send(command);

            return Ok();
        }
        
        [HttpPost("createcard")]
        public async Task<IActionResult> CreateCard([FromForm]CreateCardViewModel vm)
        {
            var createCardCommand = new CreateCardCommand(this.GetUserId(), vm.ThemeId, vm.Question, vm.Answer);

            try
            {
                await _mediator.Send(createCardCommand);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete("{themeId}")]
        public async Task<IActionResult> DeleteTheme([FromRoute] int themeId)
        {
            var command = new DeleteThemeCommand(themeId, this.GetUserId());

            await _mediator.Send(command);

            return Ok();
        }


        [HttpGet("{themeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTheme([FromRoute] int themeId)
        {
            var query = new GetThemeHeaderQuery(themeId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }


        


        [HttpGet("{themeId}/subthemes")]
        public async Task<IActionResult> GetSubThemes(int themeId)
        {
            var query = new GetUserThemesWithCardsQuery(this.GetUserId(), themeId);

            var result = await _mediator.Send(query);

            return Ok(new { themes = result, isRoot = false });
        }


        [HttpGet("getUserThemes")]
        public async Task<IActionResult> GetUserThemes()
        {
            var userId = new GetRootThemesByUserIdQuery(this.GetUserId());

            var result = await _mediator.Send(userId);
            
            return Ok(new { themes = result, isRoot = true });
        }

        [HttpPost("linkUserToTheme")]
        public async Task<IActionResult> LinkUserToTheme([FromForm]string themeName, [FromForm]TypeAccess typeAccess)
        {
            var command = new LinkUserToThemeCommand(this.GetUserName(), themeName, typeAccess);

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut("editTheme")]
        public async Task<IActionResult> EditTheme([FromForm]ThemeDto themeDto)
        {
            var command = new EditThemeCommand(themeDto, this.GetUserId());

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