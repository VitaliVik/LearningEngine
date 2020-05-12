using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Application.PipelineValidators
{
    public class PipelinePermissionValidator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            throw new NotImplementedException();
        }
    }
}
