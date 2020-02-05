using LearningEngine.Application.Exceptions;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Application.UseCase.Handler
{
    public abstract class BaseUseCaseHandler<TResult, TRequestUseCase> : IRequestHandler<TRequestUseCase, TResult>
        where TRequestUseCase: IRequest<TResult>
    {
        private readonly ITransactionUnitOfWork _uow;
        protected BaseUseCaseHandler(ITransactionUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<TResult> Execute(TRequestUseCase request)
        {
            await _uow.StartTransaction();
            try
            {
                var result = await Action(request);
                await _uow.CommitTransaction();
                return result;
            }
            catch
            {
                await _uow.RollbackTransaction();
                throw;
            }
        }

        public async Task<TResult> Handle(TRequestUseCase request, CancellationToken cancellationToken)
        {
            return await Execute(request);
        }

        protected abstract Task<TResult> Action(TRequestUseCase request);

    }
}
