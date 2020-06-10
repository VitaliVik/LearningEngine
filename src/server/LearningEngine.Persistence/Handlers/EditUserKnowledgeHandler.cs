using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Constants;
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
    public class EditUserKnowledgeHandler : IRequestHandler<EditUserKnowledgeCommand>
    {
        private readonly LearnEngineContext context;

        public EditUserKnowledgeHandler(LearnEngineContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(EditUserKnowledgeCommand request, CancellationToken cancellationToken)
        {
            var card = await context.Cards.FirstOrDefaultAsync(card => card.Id == request.CardId);

            if (card == null)
            {
                throw new CardNotFoundException();
            }

            var statistic = await context.Statistic.FirstOrDefaultAsync(stat => stat.CardId == request.CardId
                                                                         && stat.UserId == request.UserId);

            if (statistic == null)
            {
                throw new StatisticNotFoundException();
            }

            statistic.CardKnowledge += request.Value;
            context.Statistic.Update(statistic);
            await context.SaveChangesAsync();

            return default;
        }
    }
}
