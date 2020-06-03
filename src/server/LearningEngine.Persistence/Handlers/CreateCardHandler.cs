using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.Enum;
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
    public class CreateCardHandler : IRequestHandler<CreateCardCommand, int>
    {
        private readonly LearnEngineContext _context;

        public CreateCardHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var theme = await _context.Themes.FirstOrDefaultAsync(theme => theme.Id == request.ThemeId);

            if (theme == null)
            {
                throw new ThemeNotFoundException();
            }

            var card = new Card { Answer = request.Answer, Question = request.Question, ThemeId = request.ThemeId };

            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();

            return card.Id;
        }
    }
}
