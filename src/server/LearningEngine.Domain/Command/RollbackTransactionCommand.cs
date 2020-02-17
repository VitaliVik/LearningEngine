using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class RollbackTransactionCommand: IRequest
    {
        public IDbContextTransaction Transaction { get; set; }
    }
}
