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

            var command = new CreateUserThemeCommand(vm.UserName, vm.ThemeName, vm.Description, vm.IsPublic, vm.ParentThemeId);

            await _mediator.Send(command);

            return Ok();
        }
        

        [HttpDelete("{themeId}")]
        public async Task<IActionResult> DeleteTheme([FromRoute] int themeId)
        {
            var command = new DeleteThemeCommand(themeId, HttpContext.GetUserId());

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
        [AllowAnonymous]
        public async Task<IActionResult> GetSubThemes(int themeId)
        {
            var query = new GetThemeSubThemesQuery(themeId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }


        [HttpGet("getUserThemes")]
        public async Task<IActionResult> GetUserThemes()
        {
            var userId = new GetRootThemesByUserIdQuery(HttpContext.GetUserId());

            var result = await _mediator.Send(userId);
            
            return Ok(result);
        }

        [HttpPost("linkUserToTheme")]
        public async Task<IActionResult> LinkUserToTheme([FromForm]string themeName, TypeAccess typeAccess)
        {
            var command = new LinkUserToThemeCommand(HttpContext.GetUserName(), themeName, TypeAccess.Write);

            await _mediator.Send(command);

            return Ok();
        }
    }
}