using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Query;
using LearningEngine.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    public class GetThemeByCardIdHandler : IRequestHandler<GetThemeByCardIdQuery, ThemeDto>
    {
        private readonly LearnEngineContext context;

        public GetThemeByCardIdHandler(LearnEngineContext context)
        {
            this.context = context;
        }

        public async Task<ThemeDto> Handle(GetThemeByCardIdQuery request, CancellationToken cancellationToken)
        {
            var card = await context.Cards.FirstOrDefaultAsync(card => card.Id == request.CardId);

            if (card == null)
            {
                throw new CardNotFoundException();
            }

            var theme = await context.Themes.FirstOrDefaultAsync(theme => theme.Id == card.ThemeId);

            if (theme == null)
            {
                throw new ThemeNotFoundException();
            }

            return new ThemeDto
            {
                Id = theme.Id,
                Desсription = theme.Description,
                Name = theme.Name,
                IsPublic = theme.IsPublic
            };
        }
    }
}
