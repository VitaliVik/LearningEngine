using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearningEngine.Api.ViewModels;
using LearningEngine.Application.Command;

namespace LearningEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ThemeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTheme([FromForm]CreateThemeViewModel vm)
        {
            var command = new CreateThemeCommand(vm.UserName, vm.ThemeName, vm.Description, vm.IsPublic, vm.ParentThemeId);

            var result = await _mediator.Send(command);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}