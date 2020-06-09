using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEngine.Api.AppFilters;
using LearningEngine.Api.Extensions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMediator mediator;

        public NoteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{themeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNotes(int themeId)
        {
            var query = new GetThemeNotesQuery(themeId, this.GetUserId());

            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("{themeId}/note")]
        public async Task<IActionResult> AddNote([FromRoute] int themeId, [FromForm] string title, [FromForm] string content)
        {
            var command = new CreateNoteCommand(themeId, this.GetUserId(), title, content);

            await mediator.Send(command);

            return Ok();
        }
    }
}