using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Query;
using LearningEngine.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    public class GetThemeByStatisticIdHandler : IRequestHandler<GetThemeByStatisticIdQuery, ThemeDto>
    {
        private readonly LearnEngineContext _context;
        public GetThemeByStatisticIdHandler(LearnEngineContext context)
        {
            _context = context;
        }
        public async Task<ThemeDto> Handle(GetThemeByStatisticIdQuery request, CancellationToken cancellationToken)
        {
            var statistic = await _context.Statistic.FirstOrDefaultAsync
                                                    (stat => stat.Id == request.StatisticId);

            if (statistic == null)
            {
                throw new StatisticNotFoundException();
            }

            var card = await _context.Cards.FirstOrDefaultAsync(card => card.Id == statistic.CardId);

            if (card == null)
            {
                throw new CardNotFoundException();
            }

            var theme = await _context.Themes.FirstOrDefaultAsync(theme => theme.Id == card.ThemeId);

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
