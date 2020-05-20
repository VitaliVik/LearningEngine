using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEngine.Api.AppFilters;
using LearningEngine.Api.Extensions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        readonly IMediator _mediator;
        public StatisticController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ExceptionFilter]
        [HttpPost("{cardId}")]
        public async Task<IActionResult> Create([FromRoute]int cardId)
        {
            var createStatisticCommand = new CreateStatisicCommand(this.GetUserId(), cardId);

            await _mediator.Send(createStatisticCommand);

            return Ok();
        }

        [ExceptionFilter]
        [HttpPut("increaceKnowledge/{cardId}")]
        public async Task<IActionResult> IncreaceKnowledge([FromRoute]int cardId, [FromForm] int themeId)
        {
            var editStatisticCommand = new EditUserKnowledgeCommand(this.GetUserId(), themeId, cardId, 10);

            await _mediator.Send(editStatisticCommand);

            return Ok();
        }

        [ExceptionFilter]
        [HttpPut("reduceKnowledge/{cardId}")]
        public async Task<IActionResult> ReduceKnowledge([FromRoute]int cardId, [FromForm] int themeId)
        {
            var editStatisticCommand = new EditUserKnowledgeCommand(this.GetUserId(), themeId, cardId, -10);

            await _mediator.Send(editStatisticCommand);

            return Ok();
        }
    }
}