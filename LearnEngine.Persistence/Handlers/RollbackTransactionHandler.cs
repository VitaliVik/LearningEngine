using LearningEngine.Domain.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Handlers
{
    class RollbackTransactionHandler : IRequestHandler<RollbackTransactionCommand>
    {
        public async Task<Unit> Handle(RollbackTransactionCommand request, CancellationToken cancellationToken)
        {
            await request.Transaction.RollbackAsync();
            return default;
        }
    }
}
