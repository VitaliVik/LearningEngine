using LearningEngine.Domain.Command;
using LearningEngine.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    class BeginTransactionHandler : IRequestHandler<BeginTransactionCommand, IDbContextTransaction>
    {
        private readonly LearnEngineContext _context;
        public BeginTransactionHandler(LearnEngineContext context)
        {
            _context = context;
        }

        public Task<IDbContextTransaction> Handle(BeginTransactionCommand request, CancellationToken cancellationToken)
        {
            return _context.Database.BeginTransactionAsync();
        }
    }
}
