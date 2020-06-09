using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEngine.Api.AppFilters;
using LearningEngine.Api.Extensions;
using LearningEngine.Api.ViewModels;
using LearningEngine.Application.UseCase.Command;
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
    public class CardController : ControllerBase
    {
        private readonly IMediator mediator;

        public CardController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("{themeId}")]
        public async Task<IActionResult> CreateCard([FromRoute] int themeId, [FromForm] CreateCardViewModel vm)
        {
            var createCardCommand = new CreateCardAndStatisticCommand(this.GetUserId(), 
                                                                      themeId,
                                                                      vm.Question, 
                                                                      vm.Answer);

            await mediator.Send(createCardCommand);

            return Ok();
        }

        [HttpGet("{themeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCards([FromRoute] int themeId)
        {
            var query = new GetThemeCardsQuery(themeId, this.GetUserId());

            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}