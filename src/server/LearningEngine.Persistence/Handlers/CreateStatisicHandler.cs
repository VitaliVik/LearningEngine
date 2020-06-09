using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
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
    public class CreateStatisicHandler : IRequestHandler<CreateStatisicCommand>
    {
        private readonly LearnEngineContext _context;

        public CreateStatisicHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateStatisicCommand request, CancellationToken cancellationToken)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(card => card.Id == request.CardId);

            if (card == null)
            {
                throw new CardNotFoundException();
            }

            await _context.Statistic.AddAsync(new Statistic
            {
                CardId = request.CardId,
                UserId = request.UserId,
                CardKnowledge = 0.0
            });
            await _context.SaveChangesAsync();

            return default;
        }
    }
}
