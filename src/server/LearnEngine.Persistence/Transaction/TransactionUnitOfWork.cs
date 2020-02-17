using LearningEngine.Domain.Interfaces;
using LearningEngine.Persistence.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEngine.Persistence.Transaction
{
    public class TransactionUnitOfWork : ITransactionUnitOfWork
    {
        private readonly LearnEngineContext _context;
        private IDbContextTransaction transaction;
        public TransactionUnitOfWork(LearnEngineContext context)
        {
            _context = context;
        }
        public async Task CommitTransaction()
        {
            await transaction.CommitAsync();
        }

        public async Task RollbackTransaction()
        {
            await transaction.RollbackAsync();
        }

        public async Task StartTransaction()
        {
            transaction = await _context.Database.BeginTransactionAsync();
        }
    }
}
