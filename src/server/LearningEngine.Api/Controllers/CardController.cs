using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEngine.Api.Extensions;
using LearningEngine.Api.ViewModels;
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
        readonly IMediator _mediator;
        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateCard([FromForm]CreateCardViewModel vm)
        {
            var createCardCommand = new CreateCardCommand(this.GetUserId(), vm.ThemeId, vm.Question, 
                                                                            vm.Answer, TypeAccess.Write);

            try
            {
                await _mediator.Send(createCardCommand);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpGet("{themename}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCards(int themeId)
        {
            var query = new GetThemeCardsQuery(themeId, this.GetUserId(), TypeAccess.Read);

            try
            {
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}