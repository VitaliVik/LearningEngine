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
using System.Linq;

namespace LearningEngine.Persistence.Handlers
{
    class GetThemeCardsHandler : IRequestHandler<GetThemeCardsQuery, List<CardDto>>
    {
        readonly LearnEngineContext _context;
        public GetThemeCardsHandler(LearnEngineContext context)
        {
            _context = context;
        }
        public async Task<List<CardDto>> Handle(GetThemeCardsQuery request, CancellationToken cancellationToken)
        {
            var theme = await _context.Themes
                .Include(thm => thm.Cards)
                .FirstOrDefaultAsync(thm => thm.Name == request.ThemeNameId);

            if (theme != null)
            {
                var cards = theme.Cards
                    .Select(card => new CardDto { Answer = card.Answer, Question = card.Question })
                    .ToList();
                    
                return cards;
            }
            else
            {
                throw new Exception("тема не найдена");
            }
        }
    }
}
