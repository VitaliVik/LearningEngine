using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpPost("{cardId}")]
        public async Task<IActionResult> Create([FromRoute]int cardId)
        {
            var createStatisticCommand = new CreateStatisicCommand(this.GetUserId(), cardId);
            try
            {
                await _mediator.Send(createStatisticCommand);

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}