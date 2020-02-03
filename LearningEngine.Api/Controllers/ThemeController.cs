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
using LearningEngine.Application.Query;

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

            await _mediator.Send(command);

            return Ok();
        }

        //[HttpPost("{themename}/note")]
        //public async Task<IActionResult> AddNote()
        //{
        //    var command = new CreateNoteCommand();

        //    var result = await _mediator.Send(command);

        //    return Ok();
        //}


        [HttpGet("{themename}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTheme([FromRoute] string themename)
        {
            var query = new GetThemeHeaderQuery(themename);

            var result = await _mediator.Send(query);

            return Ok(result);
        }


        


        [HttpGet("{themename}/subthemes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSubThemes(string themename)
        {
            var query = new GetThemeSubThemesQuery(themename);

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}